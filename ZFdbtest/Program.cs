using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Common;
using ZF.Service.Entities;

namespace ZFdbtest
{
    class Program
    {
        static void Main(string[] args)
        {/*
            using (MyDbContext ctx = new MyDbContext())
            {
                CityEntity c = new CityEntity();
                c.Name = "昆明";
                c.IsDeleted = true;
                c.CreateDateTime = DateTime.Now;
                ctx.Cities.Add(c);
                ctx.SaveChanges();
                Console.WriteLine("ok");

            }
            */

            //测试MD5方法
            string s = "wert1341";
          //  CommonHelper ch = new CommonHelper();
            string b=CommonHelper.CalcMD5(s);
            Console.WriteLine(b);
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
