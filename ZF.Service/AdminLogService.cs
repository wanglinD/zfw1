using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.IService;
using ZF.Service.Entities;

namespace ZF.Service
{
    public class AdminLogService : IAdminLogService
    {
        public long AddNew(long adminUserId, string message)
        {
            //创建一个dbcontext对象 ctx ,用来操作Entity 对象
            using (MyDbContext ctx = new MyDbContext())
            {
                //创建Entity对象，并给log对象属性赋值
                AdminLogEntity log = new AdminLogEntity()
                { AdminUserId=adminUserId,Message=message};
                //为表赋值
                ctx.AdminUserLogs.Add(log);
                ctx.SaveChanges();
                return log.Id;
            }
        }
    }
}
