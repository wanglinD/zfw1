using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.IService
{
    //接口里方法不用实现，交给继承这接口的类来实现
    public interface IAdminLogService:IServiceSupport
    {
        //插入一条日志：adminUserId为操作用户的id,message为消息
        long AddNew(long adminUserId, string message);
        //以后做:如果做日志搜索等的话以后在增加新方法
        
    }
}
