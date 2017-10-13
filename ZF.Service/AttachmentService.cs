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
    class AttachmentService : IAttachmentService
    {
        public AttachmentDTO ToDTO(AttachmentEntity attchment)
        {
            AttachmentDTO dto = new AttachmentDTO();
            dto.CreateDateTime = attchment.CreateDateTime;
            dto.IconName = attchment.IconName;
            dto.Id = attchment.Id;
            dto.Name = attchment.Name;
            return dto;
        }
        public AttachmentDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AttachmentEntity> bs = new BaseService<AttachmentEntity>(ctx);
                var all =bs.GetAll().AsNoTracking();
                return all.ToList().Select(e => ToDTO(e)).ToArray();
            }

           
        }

        public AttachmentDTO[] GetAttachments(long houseId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //取出houseId这个房屋的Attachments
                BaseService<HouseEntity> houseBS
                    = new BaseService<HouseEntity>(ctx);
                var house = houseBS.GetAll().Include(a => a.Attachments)
                    .AsNoTracking().SingleOrDefault(h => h.Id == houseId);
                if (house == null)
                {
                    throw new ArgumentException("houseId" + houseId + "不存在");
                }
                return house.Attachments.ToList().Select(a => ToDTO(a)).ToArray();
            }

            
        }
    }
}
