using System;
using System.Data.Entity;
using System.Linq;
using ZF.Common;
using ZF.DTO;
using ZF.IService;
using ZF.Service.Entities;


namespace ZF.Service
{
    class AdminUserService : IAdminUserService
    {
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            //实体对象
            AdminUserEntity users = new AdminUserEntity();
            users.Name = name;
            users.PhoneNum = phoneNum;
            users.Email = email;
            users.CityId = cityId;
            //盐,用生成验证码的方法生成
            string salt= CommonHelper.CreateVerifyCode(5);
            users.PasswordSalt = salt;
            //把密码加密
            string passwordHash=CommonHelper.CalcMD5(password + salt);
            users.PasswordSalt = passwordHash;
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //Any确定是否包含元素,返回bool
                bool ph= bs.GetAll().Any(e => e.PhoneNum == phoneNum);
                if(ph)
                {
                    throw new ArgumentException("手机号已经存在" + phoneNum);
                }
                ctx.AdminUsers.Add(users);
                ctx.SaveChanges();

                return users.Id;
            }
                
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //查询数据库中是否只有一条与用户输入匹配的数据，没有的话返回null
                var user = bs.GetAll().SingleOrDefault(e => e.PhoneNum == phoneNum);
                if(user==null)
                {
                    return false;
                }
                //把用户输入的密码加密，与数据库中的比较
                string passwordHash = CommonHelper.CalcMD5(password + user.PasswordSalt);
                if(passwordHash==user.PasswordHash)
                {
                    return true;
                }
                return false;
            }


                
        }

        //把实体对象转换为DTO对象的方法

        public AdminUserDTO ToDTO(AdminUserEntity user)
        {
            // AdminUserEntity entity = new AdminUserEntity();
            AdminUserDTO dto = new AdminUserDTO();
            //有一个可空外键cityId
            //如果cityId为空则为总部员工
            dto.CityId = user.CityId;
            if(user.City!=null)
            {
                dto.CityName = user.City.Name; 
            }
            else
            {
                dto.CityName = "总部";
            }

            dto.Email = user.Email;
            dto.PhoneNum = user.PhoneNum;
            dto.Name = user.Name;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            dto.Id = user.Id;
            dto.CreateDateTime = user.CreateDateTime;
            return dto;


        }
        /// <summary>
        /// 获取所有后台用户
        /// </summary>
        /// <returns></returns>
        public AdminUserDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //因为这个表有city导航属性，这里用Include表示不延迟加载。
               return bs.GetAll().Include(e => e.City).AsNoTracking().ToList().Select(e => ToDTO(e)).ToArray();

            }

                
        }

        //根据cityId得到后台用户，注意延迟加载问题
        public AdminUserDTO[] GetAll(long? cityId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
              return bs.GetAll().Include(e => e.City).AsNoTracking().Where(e => e.CityId == cityId)
                    .ToList().Select(e => ToDTO(e)).ToArray();
            }

            
        }

        public AdminUserDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user =bs.GetAll().Include(e => e.City).AsNoTracking().SingleOrDefault(e=>e.Id==id);
                if(user==null)
                {
                    return null;
                }
                return ToDTO(user);
            }



              
        }
        //
        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
               var user= bs.GetAll().Include(e => e.City).AsNoTracking()
                    .Where(e => e.PhoneNum == phoneNum);
                int count = user.Count();
                if (count <= 0)
                {
                    return null;
                }
                else if (count == 1)
                {
                    return ToDTO(user.Single());
                }
                else 
                {
                    throw new ApplicationException("查找到多个管理员");
                }
            }


               
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);

               var user= bs.GetAll().Include(e => e.Roles).AsNoTracking().SingleOrDefault(e => e.Id == adminUserId);
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
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                //调用公共软删除方法
                bs.MarkDeleted(adminUserId);

            }  
        }

        public void RecordLoginError(long id)
        {

            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        //更新管理员信息
        public void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetById(id);
                if(user==null)
                {
                    throw new ArgumentException("找不到id=" + id + "的管理员");
                }
                user.Name = name;
                user.PhoneNum = phoneNum;
                user.Email = email;
                user.CityId = cityId;
                if (!string.IsNullOrEmpty(password))
                {
                    //密码加密
                    user.PasswordHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
                }
                ctx.SaveChanges();
            }
        }
    }
}
