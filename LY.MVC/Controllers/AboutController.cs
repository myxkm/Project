using Aspose.Cells.Drawing;
using Aspose.Words;
using Aspose.Words.Drawing;
using LY.Web.Core.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LY.MVC.Controllers
{




    public class AboutTest
    {
        /// <summary>
        /// Inserts a watermark into a document.
        /// </summary>
        /// <param name="doc">The input document.</param>
        /// <param name="watermarkText">Text of the watermark.</param>
        public static void InsertWatermarkText(Document doc, string watermarkText)
        {
            // Create a watermark shape. This will be a WordArt shape.
            // You are free to try other shape types as watermarks.
            Aspose.Words.Drawing.Shape watermark = new Aspose.Words.Drawing.Shape(doc, ShapeType.TextPlainText);

            // Set up the text of the watermark.
            watermark.TextPath.Text = watermarkText;
            watermark.TextPath.FontFamily = "Arial";
            watermark.Width = 500;
            watermark.Height = 100;
            // Text will be directed from the bottom-left to the top-right corner.
            watermark.Rotation = -40;
            // Remove the following two lines if you need a solid black text.
            watermark.Fill.Color = Color.Gray; // Try LightGray to get more Word-style watermark
            watermark.StrokeColor = Color.Gray; // Try LightGray to get more Word-style watermark

            // Place the watermark in the page center.
            watermark.RelativeHorizontalPosition = RelativeHorizontalPosition.Page;
            watermark.RelativeVerticalPosition = RelativeVerticalPosition.Page;
            watermark.WrapType = WrapType.None;
            watermark.VerticalAlignment = VerticalAlignment.Center;
            watermark.HorizontalAlignment = HorizontalAlignment.Center;

            // Create a new paragraph and append the watermark to this paragraph.
            Paragraph watermarkPara = new Paragraph(doc);
            watermarkPara.AppendChild(watermark);

            // Insert the watermark into all headers of each document section.
            foreach (Section sect in doc.Sections)
            {
                // There could be up to three different headers in each section, since we want
                // the watermark to appear on all pages, insert into all headers.
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderPrimary);
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderFirst);
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderEven);
            }
        }

        private static void InsertWatermarkIntoHeader(Paragraph watermarkPara, Section sect, HeaderFooterType headerType)
        {
            HeaderFooter header = sect.HeadersFooters[headerType];

            if (header == null)
            {
                // There is no header of the specified type in the current section, create it.
                header = new HeaderFooter(sect.Document, headerType);
                sect.HeadersFooters.Add(header);
            }

            // Insert a clone of the watermark into the header.
            header.AppendChild(watermarkPara.Clone(true));
        }

    }


    [AllowAnonymous]
    public class AboutController : Controller
    {
        // GET: Admin/About
        public ActionResult About()
        {

            Aspose.Words.Document document = new Aspose.Words.Document(@"D:\个人\Demo\Project\LY.MVC\Utility\物业项目基础情况一览表.doc");
              AboutTest.InsertWatermarkText(document, "CONFIDENTIAL");
            document.Save(@"D:\个人\Demo\Project\LY.MVC\Utility\物业项目基础情况一览表.pdf", Aspose.Words.SaveFormat.Pdf);
            var filename = Server.MapPath("~/Utility/合同模板1.xls");
            Aspose.Cells.Workbook excel = new Aspose.Cells.Workbook(filename);
            excel.Save(Server.MapPath("~/Utility/物业项目基础情况一览表1.Pdf"), Aspose.Cells.SaveFormat.Pdf);
            //HttpContext.Cache
            //HttpContext.Session
            var query = HttpContext.Request.QueryString;
            //HttpContext.Cache
            //HttpContext.Session
            //HttpContext.Application  


            return View();
        }
        [ChildActionOnly]
        public ActionResult Contact()
        {



            return View();
        }
        public ActionResult Contents()
        {
            ControllerContext controllerContext = new ControllerContext();
            new NewtonsoftJsonResult("{data:data}").ExecuteResult(controllerContext);
            return View();
        }
    }
}