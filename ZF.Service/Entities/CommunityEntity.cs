using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Service.Entities
{
   public class CommunityEntity:BaseEntity
    {
        public string Name { get; set; }
        public long RegionId { get; set; }
        public string Location { get; set; }
        public string Traffic { get; set; }
        public int? BuiltYear { get; set; }
        //在一对多关系中，导航属性配置在多端
        public virtual RegionEntity Region { get; set; }
    }
}
