using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Service.Entities
{
    //系统配置表，根据需求文档定义父类中没有定义到的类
    public class SettingEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
