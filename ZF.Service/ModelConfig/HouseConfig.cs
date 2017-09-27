using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class HouseConfig:EntityTypeConfiguration<HouseEntity>
    {
        public HouseConfig ()
        {
            ToTable("T_Houses");
            HasRequired(h => h.Community).WithMany().HasForeignKey(h => h.CommunityId).WillCascadeOnDelete(false);
            HasRequired(h => h.RoomType).WithMany().HasForeignKey(h => h.RoomTypeId).WillCascadeOnDelete(false);
            HasRequired(h => h.Status).WithMany().HasForeignKey(h => h.StatusId).WillCascadeOnDelete(false);
            HasRequired(h => h.Type).WithMany().HasForeignKey(h => h.TypeId).WillCascadeOnDelete(false);
            HasRequired(h => h.DecorateStatus).WithMany().HasForeignKey(h => h.DecorateStatusId).WillCascadeOnDelete(false);
            Property(e => e.Address).HasMaxLength(200).IsRequired();
            Property(e => e.Direction).HasMaxLength(10).IsRequired();
            Property(e => e.OwnerName).HasMaxLength(20).IsRequired();
            Property(e => e.Description).IsOptional();
            Property(h => h.OwnerPhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);
        }
    }
}
