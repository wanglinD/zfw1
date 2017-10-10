using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.DTO;

namespace ZF.IService
{
    //后台用户需要的接口
     public interface IAdminUserService:IServiceSupport
    {
        //增加一个后台用户
        long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId);
        //更新后台用户的信息
        void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId);
        //获取cityId这个城市下的管理员
        AdminUserDTO[] GetAll(long? cityId);
        //获取所有的管理员
        AdminUserDTO[] GetAll();
        //根据id获取DTO
        AdminUserDTO[] GetById(long id);
        //根据手机号获取DTO
        AdminUserDTO[] GetByPhoneNum(string phoneNum);

        //检查用户的用户名密码是否正确
        bool CheckLogin(string phoneNum, string password);
        //软删除
        void MarkDeleted(long adminUserId);

        //判断adminUserId这个用户是否有permissionName这个权限
        //HasPermission(3,"User.Add")

        bool HasPermission(long adminUserId, string permissionName);

        void RecordLoginError(long id);//记录错误登录一次
        void ResetLoginError(long id);//重置登录错误信息
    }
}
