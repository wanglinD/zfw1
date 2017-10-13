using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.DTO;
using ZF.IService;
using ZF.Service;
using ZF.Service.Entities;

namespace ZF.Service
{
    public class AttachmentService : IAttachmentService
    {

        public AttachmentDTO ToDTO(AttachmentEntity attachment)
        {
            AttachmentDTO dto = new AttachmentDTO();
            dto.CreateDateTime = attachment.CreateDateTime;
            dto.IconName = attachment.IconName;
            dto.Id = attachment.Id;
            dto.Name = attachment.Name;
            return dto;
        }
        public AttachmentDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<AttachmentEntity> bs = new BaseService<AttachmentEntity>(ctx);
               var attachments= bs.GetAll().AsNoTracking();
                return attachments.ToList().Select(e => ToDTO(e)).ToArray();
            }

              
        }

        public AttachmentDTO[] GetAttachments(long houseId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<HouseEntity> bs = new BaseService<HouseEntity>(ctx);
                var house = bs.GetAll().AsNoTracking().Include(e => e.Attachments)
                      .SingleOrDefault(e => e.Id == houseId);
                if (house == null)
                {
                    throw new ArgumentException("没有找到id=" + houseId + "的房子");
                }
                return house.Attachments.ToList().Select(e => ToDTO(e)).ToArray();
            }

        }
    }
}
