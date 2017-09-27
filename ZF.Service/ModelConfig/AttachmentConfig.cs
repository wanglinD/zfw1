using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.Service.Entities;

namespace ZF.Service.ModelConfig
{
    class AttachmentConfig:EntityTypeConfiguration<AttachmentEntity>
    {
        public AttachmentConfig()
        {
            ToTable("T_Attachments");
            HasMany(e => e.Houses).WithMany(e => e.Attachments).Map(t => t.ToTable("T_HouseAttachments")
                .MapLeftKey("AttachmentId").MapRightKey("HousId"));
            Property(p => p.Name).HasMaxLength(50).IsRequired();
            Property(p => p.IconName).HasMaxLength(50).IsRequired().IsUnicode(false);

        }
    }
}
