using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.DTO
{
    //建一个用于被继承的抽像类，继承了这个抽象类的子类都共同拥有这连个属性
    public abstract class BaseDTO
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }

    }
}
