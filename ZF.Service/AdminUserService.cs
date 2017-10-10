using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Common;
using ZF.DTO;
using ZF.IService;
using ZF.Service.Entities;

namespace ZF.Service
{
    public class AdminUserService : IAdminUserService
    {
        //增加后台用户
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            AdminUserEntity user = new AdminUserEntity();
            user.CityId = cityId;
            user.Email = email;
            user.Name = name;
            user.PhoneNum = phoneNum;
            string salt = CommonHelper.CreateVerifyCode(5);//盐
            user.PasswordSalt = salt;
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                bool exists = bs.GetAll().Any(u => u.PhoneNum == phoneNum);
                if(exists)
                {
                    throw new ArgumentException("手机号已存在" + phoneNum);
                }
                //得到用户的输入，保存
                //把Entity对象保存到AdminUsers表中
                ctx.AdminUsers.Add(user);
                ctx.SaveChanges();
                return user.Id;
            }


        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //根据用户输入的电话查询数据库数据
                var user = bs.GetAll().SingleOrDefault(u => u.PhoneNum == phoneNum);
                if(user==null)
                {
                    return false;
                }
                //得到用户的加密密码
                string dbHash = user.PasswordHash;
                //把用户输入的密码加盐加密后与数据库中的比较
                string userHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
                //返回值是bool类型
                return userHash == dbHash;
            }

               
        }
        /// <summary>
        /// 将Entity对象转为DTO对象
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private AdminUserDTO ToDTO(AdminUserEntity user)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CityId = user.CityId;
            if (user.City != null)
            {
                dto.CityName = user.City.Name;//需要Include提升性能
                //如鹏总部（北京）、如鹏网上海分公司、如鹏广州分公司、如鹏北京分公司
            }
            else
            {
                dto.CityName = "总部";
            }

            dto.CreateDateTime = user.CreateDateTime;
            dto.Email = user.Email;
            dto.Id = user.Id;
            dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.Name = user.Name;
            dto.PhoneNum = user.PhoneNum;
            return dto;
        }
        /// <summary>
        /// 得到后台用户数据
        /// </summary>
        /// <returns></returns>
        public AdminUserDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //在这里需要拿到用户所属的城市，可以看AdminUserDTO属性，因为这个表有一个外键CityId,导航属性City，会涉及到延迟加载问题

                //AsNoTracking() 查询到的数据只是用来显示。
                return bs.GetAll().Include(e => e.City).AsNoTracking().ToList().Select(e => ToDTO(e)).ToArray();
            }

        }

        public AdminUserDTO[] GetAll(long? cityId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //取出数据
                var all = bs.GetAll().Include(e => e.City).AsNoTracking().Where(e => e.CityId == cityId);
                //把数据转换为DTO返回
                return all.ToList().Select(e => ToDTO(e)).ToArray();
            }

        }

        /// <summary>
        /// 根据id查到用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdminUserDTO[] GetById(long id)
        {

            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                   var user =bs.GetAll().Include(e => e.City).AsNoTracking()
                    .SingleOrDefault(u => u.Id == id);
                if(user==null)
                {
                    return null;
                }
                return ToDTO(user);

            }

             
        }
        /// <summary>
        /// 根据手机号查询用户信息
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public AdminUserDTO[] GetByPhoneNum(string phoneNum)
        {

            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var users = bs.GetAll().Include(e => e.City).AsNoTracking()
                    .Where(e => e.PhoneNum == phoneNum);
                int count = users.Count();
                if (count <= 0)
                {
                    return null;
                }
                else if (count == 1)
                {
                    return ToDTO(users.Single());
                }
                else
                {
                    throw new ApplicationException("找到多个手机号为" + phoneNum + "的管理员");
                }
            }
                
        }
        /// <summary>
        /// 判断adminUserId 的用户是否具有PermissionName权限
        /// </summary>
        /// <param name="adminUserId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetAll().Include(u => u.Roles)
                    .AsNoTracking().SingleOrDefault(u => u.Id == adminUserId);
                //var user = bs.GetById(adminUserId);
                if (user == null)
                {
                    throw new ArgumentException("找不到id=" + adminUserId + "的用户");
                }
                //每个Role都有一个Permissions属性
                //Roles.SelectMany(r => r.Permissions)就是遍历Roles的每一个Role
                //然后把每个Role的Permissions放到一个集合中
                //IEnumerable<PermissionEntity>
                return user.Roles.SelectMany(r => r.Permissions)
                    .Any(p => p.Name == permissionName);
            
               }
        }
          
        

        public void MarkDeleted(long adminUserId)
        {
            throw new NotImplementedException();
        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId)
        {
            throw new NotImplementedException();
        }
    }
}
