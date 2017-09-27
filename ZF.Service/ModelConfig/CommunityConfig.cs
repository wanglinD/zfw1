using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class CommunityConfig:EntityTypeConfiguration<CommunityEntity>
    {
        public CommunityConfig()
        {
            ToTable("T_Communities");

            this.HasRequired(e => e.Region).WithMany().HasForeignKey(c => c.RegionId).WillCascadeOnDelete(false);
            Property(p => p.Name).HasMaxLength(50).IsRequired();
            Property(p => p.Location).HasMaxLength(1024).IsRequired();
        }
    }
}
