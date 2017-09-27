using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//DBfirst,建数据库
namespace ZF.Service.Entities
{   
    //抽象类
    //父类：定义大家都有的属性，Id、CreateDateTime(数据创建时间)、IsDeleted(数据软删除标记)初始为false 未删除
    //在这里的属性都应该定义成public
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        //C# 6.0语法 赋初值
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
