using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class AdminLogConfig:EntityTypeConfiguration<AdminLogEntity>
    {
        public AdminLogConfig()
        {
            ToTable("AdminLogds");
            //
            HasRequired(l => l.AdminUser).WithMany()
                .HasForeignKey(e => e.AdminUserId).WillCascadeOnDelete(false);
            //Message这个属性不能为空
            Property(e => e.Message).IsRequired();
        }
    }
}
