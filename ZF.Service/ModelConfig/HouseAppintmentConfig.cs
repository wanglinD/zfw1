using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class HouseAppintmentConfig:EntityTypeConfiguration<HouseAppointmentEntity>
    {
        public HouseAppintmentConfig()
        {
            ToTable("T_HouseAppointments");
            HasOptional(e => e.User).WithMany().HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            HasRequired(a => a.House).WithMany().HasForeignKey(a => a.HouseId).WillCascadeOnDelete(false);
            HasOptional(b => b.FollowAdminUser).WithMany().HasForeignKey(b => b.FollowAdminUserId).WillCascadeOnDelete(false);
            Property(p => p.Name).HasMaxLength(50).IsRequired();
            Property(h => h.PhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);
            Property(h => h.Status).IsRequired().HasMaxLength(20);
            Property(h => h.RowVersion).IsRequired().IsRowVersion();
        }
    }
}
