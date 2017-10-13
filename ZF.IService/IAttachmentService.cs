using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZF.DTO;

namespace ZF.IService
{
    /// <summary>
    ///房屋配置，设施
    /// </summary>
     public interface IAttachmentService:IServiceSupport
    {
        /// <summary>
        /// 获取房子拥有的全部设施
        /// </summary>
        /// <returns></returns>
        AttachmentDTO[] GetAll();
        /// <summary>
        /// 根据房屋Id获取对应的配置
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        AttachmentDTO[] GetAttachments(long houseId);
    }
}
