using LY.UploadFile.Iterface;
using LY.Web.Core.IOC;
using System.IO;
using System.Web;
using System.Web.Http;
using Unity;

namespace E6.Public.UploadFile.Web.Controllers
{
    public class UploadFileController : ApiController
    {
        /// <summary>
        /// 文件的路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [AcceptVerbs("get", "post")]
        public dynamic Index(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = @"C:\Users\14771\Desktop\试卷模板.xls";
            }
            FileInfo f = new FileInfo(filePath);
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var container = DIFactory.GetContainer();
            IUploadService service = container.Resolve<IUploadService>();
            var res = service.Upload(filePath);
            return Json(res);
        }
        [AcceptVerbs("get", "post")]
        public dynamic Upload()
        {
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            var fileUpload = new HttpPostedFileWrapper(file);
            var container = DIFactory.GetContainer();
            IUploadService service = container.Resolve<IUploadService>();
            var res = service.Upload(fileUpload);
            return Json(res);
        }
    }

}
