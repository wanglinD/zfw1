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
    public class HouseService : IHouseService
    {

        public HouseDTO ToDTO(HouseEntity house)
        {
            HouseDTO dto = new HouseDTO();
            dto.Address = house.Address;
            dto.Area = house.Area;
            dto.AttachmentIds = house.Attachments.Select(e => e.Id).ToArray();
            dto.CheckInDateTime = house.CheckinDateTime;
            dto.CityId = house.Community.Region.CityId;
            dto.CityName = house.Community.Region.City.Name;
            dto.CommunityBuiltYear = house.Community.BuiltYear;
            dto.CommunityId = house.CommunityId;
            dto.CommunityLocation = house.Community.Location;
            dto.CommunityName = house.Community.Name;
            dto.CommunityTraffic = house.Community.Traffic;
            dto.CreateDateTime = house.CreateDateTime;
            dto.DecorateStatusId = house.DecorateStatusId;
            dto.DecorateStatusName = house.DecorateStatus.Name;
            dto.Description = house.Description;
            dto.Direction = house.Direction;
            var firstPic = house.HousePics.FirstOrDefault();
            if (firstPic != null)
            {
                dto.FirstThumbUrl = firstPic.ThumbUrl;
            }
            dto.FloorIndex = house.FloorIndex;
            dto.Id = house.Id;
            dto.LookableDateTime = house.LookableDateTime;
            dto.MonthRent = house.MonthRent;
            dto.OwnerName = house.OwnerName;
            dto.OwnerPhoneNum = house.OwnerPhoneNum;
            dto.RegionId = house.Community.RegionId;
            dto.RegionName = house.Community.Region.Name;
            dto.RoomTypeId = house.RoomTypeId;
            dto.RoomTypeName = house.RoomType.Name;
            dto.StatusId = house.StatusId;
            dto.StatusName = house.Status.Name;
            dto.TotalFloorCount = house.TotalFloorCount;
            dto.TypeId = house.TypeId;
            dto.TypeName = house.Type.Name;
            return dto;


        }
        public long AddNew(HouseAddNewDTO house)
        {
            HouseEntity entity = new HouseEntity();
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AttachmentEntity> attBs = new BaseService<AttachmentEntity>(ctx);
                //拿到house.AttachmentIds 为主键的房屋配套设施
                var atts = attBs.GetAll().Where(a => house.AttachmentIds.Contains(a.Id));
                foreach(var att in atts)
                {
                    entity.Attachments.Add(att);
                }
                entity.Address = house.Address;
                entity.Area = house.Area;
                entity.CheckinDateTime = house.CheckInDateTime;
                entity.CommunityId = house.CommunityId;
                entity.DecorateStatusId = house.DecorateStatusId;
                entity.Description = house.Description;
                entity.Direction = house.Direction;
                entity.FloorIndex = house.FloorIndex;
                entity.LookableDateTime = house.LookableDateTime;
                entity.MonthRent = house.MonthRent;
                entity.OwnerName = house.OwnerName;
                entity.OwnerPhoneNum = house.OwnerPhoneNum;
                entity.RoomTypeId = house.RoomTypeId;
                entity.StatusId = house.StatusId;
                entity.TotalFloorCount = house.TotalFloorCount;
                entity.TypeId = house.TypeId;
                ctx.Houses.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }


               
        }

        public long AddNewHousePic(HousePicDTO housePic)
        {
            HousepicEntity entity = new HousepicEntity();
            entity.HouseId = housePic.HouseId;
            entity.ThumbUrl = housePic.ThumbUrl;
            entity.Url = housePic.Url;
            using (MyDbContext ctx = new MyDbContext())
            {
                ctx.HousePics.Add(entity);
                ctx.SaveChanges();
                return entity.Id;
            }


              
        }

        public void DeleteHousePic(long housePicId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //复习EF状态转换
                /*
                HousepicEntity entity = new HousepicEntity();
                entity.Id = housePicId;
                ctx.Entry(entity).State = EntityState.Deleted;
                ctx.SaveChanges();
                */
                //不用EF状态转换
                var entity = ctx.HousePics.SingleOrDefault(e => e.IsDeleted == false && e.Id == housePicId);
                if (entity != null)
                {
                    ctx.HousePics.Remove(entity);
                    ctx.SaveChanges();
                }
            }


                
        }

        public HouseDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public HouseDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<HouseEntity> bs = new BaseService<HouseEntity>(ctx);

            }

                throw new NotImplementedException();
        }

        public long GetCount(long cityId, DateTime startDateTime, DateTime endDateTime)
        {
            throw new NotImplementedException();
        }

        public HouseDTO[] GetPagedData(long cityId, long typeId, int pageSize, int currentIndex)
        {
            throw new NotImplementedException();
        }

        public HousePicDTO[] GetPics(long houseId)
        {
            throw new NotImplementedException();
        }

        public int GetTodayNewHouseCount(long cityId)
        {
            throw new NotImplementedException();
        }

        public long GetTotalCount(long cityId, long typeId)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(long id)
        {
            throw new NotImplementedException();
        }

        public HouseSearchResult Search(HouseSearchOptions options)
        {
            throw new NotImplementedException();
        }

        public void Update(HouseDTO house)
        {
            throw new NotImplementedException();
        }
    }
}
