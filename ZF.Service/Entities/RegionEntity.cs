using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Service.Entities
{
    //区域表因为有一个与Cities表相关连的外键，
    //在这里需要用到导航属性，
    //一对多关系，一个城市有多个区
    //导航属性配置到多端，并在相对应的Config中配置
   public class RegionEntity:BaseEntity
    {
        public string Name { get; set; }
        public long CityId { get; set; }
        public virtual CityEntity City { get; set; }
    }
}
