using LY.UploadFile.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace E6.Public.UploadFile.Web.Controllers
{
    public class UploadController : Controller
    {
        //http://localhost:8020/UploadFiles
        string _domain = ConfigurationManager.AppSettings["Domain"].TrimEnd('/');
        string _saveFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["SaveFolder"].TrimEnd('\\'));
        string _fileSuffix = ConfigurationManager.AppSettings["FileSuffix"].TrimEnd(',').ToLower();
        public JsonResult Index()
        {
            var tenantID = Request["TenantID"] ?? "001";
            var type = Request["Type"] ?? "dev";
            var module = Request["Module"] ?? "ly";
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (String.IsNullOrEmpty(file.FileName))
                    {
                        return new JsonResult { Data = new ResourceInfo { Message = "file name is null or empty" } };
                    }

                    if (!IsFileValid(file.FileName))
                    {
                        string message = "file is not allowed to upload, the valid file suffix is " + _fileSuffix;
                        return new JsonResult { Data = new ResourceInfo { Message = message } };
                    }
                    string ext = Path.GetExtension(file.FileName).ToLower();
                    var FileType = ext.Remove(0, 1);
                    string fileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), ext);
                    ext = ext.Replace('.', ' ').Trim();
                    string strDate = DateTime.Now.ToString("yyyyMMdd");
                    string folder = string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\{5}", _saveFolder, type, tenantID, module, ext, strDate);
                    string fullPath = string.Format("{0}\\{1}", folder, fileName);

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    file.SaveAs(fullPath);
                    string url = string.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}", _domain, type, tenantID, module, ext, strDate, fileName);
                    return new JsonResult { Data = new ResourceInfo { Url = url, Name = fileName, AttachmentName = file.FileName, AttachSize = file.ContentLength, AttachType = file.ContentType, FileType = FileType, Status = 1, Message = "success" } };
                }
                else
                {
                    return new JsonResult { Data = new ResourceInfo { Message = "hava no valid file" } };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new ResourceInfo { Message = ex.Message } };
            }
        }
        private bool IsFileValid(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string ext = Path.GetExtension(fileName).ToLower().TrimStart('.');
                return _fileSuffix.Split(',').Any(x => x.Equals(ext));
            }

            return false;
        }
    }

}
