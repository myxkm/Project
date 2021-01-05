using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LY.UploadFile.Model
{
    /// <summary>
    /// 统一存储的静态文件信息
    /// </summary>
    public class ResourceInfo
    {
        /// <summary>
        /// 上传文件处理完成返回的状态，1：成功，0：失败
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 保存后的文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 完整的文件访问URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 上传完成后的说明信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 附件原始名称
        /// </summary>
        public string AttachmentName { get; set; }
        /// <summary>
        /// 附件扩展名类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string AttachType { get; set; }
        /// <summary>
        /// 附件大小
        /// </summary>
        public int AttachSize { get; set; }
    }
}