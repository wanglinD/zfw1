﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class IdNameConfig:EntityTypeConfiguration<IdNameEntity>
    {
        public IdNameConfig()
        {
            ToTable("T_IdNames");
            Property(p => p.Name).IsRequired().HasMaxLength(1024);
            Property(p => p.TypeName).IsRequired().HasMaxLength(1024);

        }
    }
}
