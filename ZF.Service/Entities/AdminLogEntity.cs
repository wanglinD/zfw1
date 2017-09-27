using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Service.Entities
{
   public class AdminLogEntity:BaseEntity
    {
        public long AdminUserId { get; set; }
        public string Message { get; set; }
        public virtual AdminUserEntity AdminUser { get; set; }
    }
}
