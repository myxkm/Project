using LY.UploadFile.Iterface;
using LY.UploadFile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LY.UploadFile.Service
{

    /// <summary>
    /// 该服务将文件上传到统一的存储服务器，并返回文件名、访问地址和状态信息
    /// </summary>
    public class UploadService : IUploadService
    {
        //存储服务器上传URL
        string _uploadUrl;

        public UploadService()
        {
            _uploadUrl = System.Configuration.ConfigurationManager.AppSettings["UploadUrl"].TrimEnd('/');
        }

        

        /// <summary>
        /// 将客服端上传的文件转存到存储服务器
        /// </summary>
        /// <param name="file">客户端上传的文件</param>
        /// <returns></returns>
        public ResourceInfo Upload(HttpPostedFileBase file)
        {
            return Post(file.InputStream, file.FileName);
        }

        /// <summary>
        /// 将本地文件上传到存储服务器
        /// </summary>
        /// <param name="filePath">文件完整路径</param>
        /// <returns></returns>
        public ResourceInfo Upload(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                return Post(stream, file.Name);
            }
            catch (Exception ex)
            {
                return new ResourceInfo { Message = ex.Message };
            }
        }

        /// <summary>
        /// 将文件流上传到存储服务器
        /// </summary>
        /// <param name="stream">文件字节流</param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public ResourceInfo Upload(Stream stream, string fileName)
        {
            return Post(stream, fileName);
        }

        #region 上传文件
        private ResourceInfo Post(Stream inStream, string fileName)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(_uploadUrl);
                string boundary = string.Format("----{0}", DateTime.Now.Ticks.ToString("x").ToUpper());

                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                request.Method = "POST";

                string header =
                    string.Format("Content-Disposition: form-data; name=\"iptUpload\"; filename=\"{0}\"\r\nContent-Type: application/octet-stream\r\n\r\n",
                    fileName);

                var headerBytes = Encoding.UTF8.GetBytes(header);
                var beginBoundary = Encoding.ASCII.GetBytes(string.Format("--{0}\r\n", boundary));
                var endBoundary = Encoding.ASCII.GetBytes(string.Format("\r\n--{0}--\r\n", boundary));

                request.ContentLength = beginBoundary.Length + headerBytes.Length + inStream.Length + endBoundary.Length;

                var requestStream = request.GetRequestStream();
                requestStream.Write(beginBoundary, 0, beginBoundary.Length);
                requestStream.Write(headerBytes, 0, headerBytes.Length);

                var buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = inStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                requestStream.Write(endBoundary, 0, endBoundary.Length);
                requestStream.Close();

                //接收响应  
                response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string content = sr.ReadToEnd();
                sr.Close();
                var resoueceInfo = JsonConvert.DeserializeObject<ResourceInfo>(content);

                return resoueceInfo;
            }
            catch (WebException ex)
            {
                return new ResourceInfo { Message = ex.Message };
            }
            finally
            {
                request.Abort();
                response.Close();
            }
        }
        #endregion
    }
}
