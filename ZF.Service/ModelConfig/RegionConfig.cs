﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class RegionConfig:EntityTypeConfiguration<RegionEntity>
    {
        public RegionConfig()
        {
            ToTable("T_Regions");
            HasRequired(r => r.City).WithMany().HasForeignKey(r => r.CityId).WillCascadeOnDelete(false);
            Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
