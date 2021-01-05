using LY.UploadFile.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LY.UploadFile.Iterface
{
     
        /// <summary>
        /// 该服务将文件上传到统一的存储服务器，并返回文件名、访问地址和状态信息
        /// </summary>
        public interface IUploadService
        {
            /// <summary>
            /// 将客服端上传的文件转存到存储服务器
            /// </summary>
            /// <param name="file">客户端上传的文件</param>
            /// <returns></returns>
            ResourceInfo Upload(HttpPostedFileBase file);

            /// <summary>
            /// 将本地文件上传到存储服务器
            /// </summary>
            /// <param name="filePath">文件完整路径</param>
            /// <returns></returns>
            ResourceInfo Upload(string filePath);

            /// <summary>
            /// 将文件流上传到存储服务器
            /// </summary>
            /// <param name="stream">文件字节流</param>
            /// <param name="fileName">文件名称</param>
            /// <returns></returns>
            ResourceInfo Upload(Stream stream, string fileName);
        }
    }
