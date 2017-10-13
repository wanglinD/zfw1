using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.DTO;
using ZF.IService;
using ZF.Service.Entities;

namespace ZF.Service
{
    class CityService : ICityService
    {
        //实体转DTO

        public CityDTO ToDTO(CityEntity city)
        {
            CityDTO dto = new CityDTO();
            dto.CreateDateTime = city.CreateDateTime;
            dto.Id = city.Id;
            dto.Name = city.Name;
            return dto;
        }

        public long AddNew(string CityName)
        {
            //新增城市，判断是否已经存在
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<CityEntity> bs = new BaseService<CityEntity>(ctx);
                bool city=bs.GetAll().Any(e => e.Name == CityName);
                if(city)
                {
                    throw new ArgumentException("城市已经存在！");
                }
                CityEntity citys = new CityEntity();
                citys.Name = CityName;
                ctx.Cities.Add(citys);
                ctx.SaveChanges();
                return citys.Id;
            }
            
        }

        public CityDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<CityEntity> bs = new BaseService<CityEntity>(ctx);
               return bs.GetAll().AsNoTracking().ToList().Select(e => ToDTO(e)).ToArray();


            }

            
        }

        public CityDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<CityEntity> bs = new BaseService<CityEntity>(ctx);
                //var cities =bs.GetAll().AsNoTracking().SingleOrDefault(e => e.Id == id);
                var cities = bs.GetById(id);
                if(cities==null)
                {
                    throw new ArgumentException("找不到id=" + id + "的城市");
                }
                return ToDTO(cities);
            }

                
        }
    }
}
