﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.DTO
{
     public class CommunityDTO:BaseDTO
    {
        public string Name { get; set; }
        public long RegionId { get; set; }
        public string Location { get; set; }
        public string Traffic { get; set; }
        public int BuiltYear { get; set; }
        
    }
}
