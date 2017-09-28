using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class UserConfig:EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            ToTable("T_Users");
            HasRequired(r => r.City).WithMany().HasForeignKey(u => u.CityId).WillCascadeOnDelete(false);
            Property(p => p.PasswordHash).IsRequired().HasMaxLength(100);
            Property(p => p.PasswordSalt).IsRequired().HasMaxLength(20);
            Property(p => p.PhoneNum).IsRequired().HasMaxLength(20).IsUnicode(false);
        }
    }
}
