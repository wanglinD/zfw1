using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class CityConfig:EntityTypeConfiguration<CityEntity>
    {
        public CityConfig()
        {
            ToTable("T_Cities");
            Property(e => e.Name).HasMaxLength(20).IsRequired();
        }
    }
}
