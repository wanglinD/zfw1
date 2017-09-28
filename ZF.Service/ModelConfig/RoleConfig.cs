using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class RoleConfig:EntityTypeConfiguration<RoleEntity>
    {
        public RoleConfig()
        {
            ToTable("T_Roles");
            HasMany(r => r.Permissions).WithMany(r => r.Roles).Map(r => r.ToTable("T_RolePermission")
                .MapLeftKey("RoleId").MapRightKey("PermissionId"));
            Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
