using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class AdminUserConfig:EntityTypeConfiguration<AdminUserEntity>
    {
        public AdminUserConfig()
        {
            ToTable("T_AdminUsers");
            HasOptional(c => c.City).WithMany().HasForeignKey(c => c.CityId)
                .WillCascadeOnDelete(false);
            HasMany(c => c.Roles).WithMany(r => r.AdminUsers).Map(m => m.ToTable("T_AdminUserRoles")
                .MapLeftKey("AdminUserId").MapRightKey("RoleId"));
            //IsUnicode(false) :Name 对应的数据库类型是varchar类型，而不是nvarchar.
            Property(e => e.Name).HasMaxLength(50).IsRequired().IsUnicode(false);
            Property(e => e.PhoneNum).HasMaxLength(50).IsRequired().IsUnicode(false);
            Property(e => e.PasswordHash).HasMaxLength(250).IsRequired().IsUnicode(false);
            Property(e => e.PasswordSalt).HasMaxLength(50).IsRequired().IsUnicode(false);
            Property(e => e.Email).HasMaxLength(50).IsRequired().IsUnicode(false);


        }
    }
}
