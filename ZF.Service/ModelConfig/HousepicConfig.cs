using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class HousepicConfig:EntityTypeConfiguration<HousepicEntity>
    {
        public HousepicConfig()
        {
            ToTable("T_Housepics");
            HasRequired(e => e.Houses).WithMany().HasForeignKey(e => e.HouseId).WillCascadeOnDelete(false);
            Property(e => e.Url).HasMaxLength(200).IsRequired();
            Property(e => e.ThumbUrl).HasMaxLength(200).IsRequired();
         }
    }
}
