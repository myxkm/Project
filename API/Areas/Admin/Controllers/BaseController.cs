using LY.Bussiness.Interface;
using LY.Framework.ImageHelper;
using LY.Remote.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public ICategoryService _iCategoryService = null;
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="iCategoryService"></param>
        public BaseController(ICategoryService categoryService)
        {
            this._iCategoryService = categoryService;
        }

        /// <summary>
        /// 登入验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out string code);
            HttpContext.Session["Code"] = code;
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            return File(stream.ToArray(), "Image/png");
        }        
}        }