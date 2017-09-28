using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZFdbtest
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.ReadKey();
        }
    }
}
