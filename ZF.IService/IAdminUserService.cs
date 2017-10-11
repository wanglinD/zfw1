using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.DTO;

namespace ZF.IService
{
     public interface IAdminUserService:IServiceSupport
    {
        //增加后台用户
        long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId);
        //更新用户信息
        void UpdateAdminUser(long id, string name, string phoneNum, string password, string email, long? cityId);
        //获取cityId城市下的用户信息
        AdminUserDTO[] GetAll(long? cityId);
        //获取全部信息
        AdminUserDTO[] GetAll();
        //根据id获取用户信息
        AdminUserDTO GetById(long id);
        //根据电话号码获取用户信息
        AdminUserDTO GetByPhoneNum(string phoneNum);
        //判断用户的登录手机号和密码是否正确
        bool CheckLogin(string phoneNum, string password);
        //软删除
        void MarkDeleted(long adminUserId);
        //判断adminUserid用户是否有permissionName权限
        bool HasPermission(long adminUserId, string permissionName);
        //记录登录错误信息
        void RecordLoginError(long id);
        //重置登录错误信息
        void ResetLoginError(long id);
    }
}
