using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace mv_my_ms.Controllers
{
    public class RARComman
    {
        /// <summary>
        /// 实例化FastZip
        /// </summary>
        public static FastZip fz = new FastZip();
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="zipFilePath">压缩文件的路径与名称</param>
        /// <param name="FilePath">被压缩的文件路径</param>
        /// <param name="ZipPWD">解压密码（null代表无密码）</param>
        /// <returns></returns>
        public static string FileToZip(string zipFilePath, string FilePath, string ZipPWD)
        {
            string state = "Fail...";
            try
            {
                FileInfo fi = new FileInfo(FilePath);
                string filename = fi.Name;
                string dirname = fi.DirectoryName;
                fz.Password = ZipPWD;
                fz.CreateZip(zipFilePath, dirname, false, filename);

                state = "Success !";
            }
            catch (Exception ex)
            {
                state += "," + ex.Message;
            }
            return state;
        }
        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="DirPath">被压缩的文件夹路径</param>
        /// <param name="ZipPath">压缩文件夹的路径与名称</param>
        /// <param name="ZipPWD">解压密码（null代表无密码）</param>
        /// <returns></returns>
        public static string DirToZip(string DirPath, string ZipPath, string ZipPWD)
        {
            string state = "Fail...";
            try
            {
                fz.Password = ZipPWD;
                fz.CreateZip(ZipPath, DirPath, false, null);

                state = "Success !";
            }
            catch (Exception ex)
            {
                state += "," + ex.Message;
            }
            return state;
        }

        /// <summary>
        /// 解压Zip
        /// </summary>
        /// <param name="DirPath">解压后存放路径</param>
        /// <param name="ZipPath">Zip的存放路径</param>
        /// <param name="ZipPWD">解压密码（null代表无密码）</param>
        /// <returns></returns>
        public static string Compress(string DirPath, string ZipPath, string ZipPWD)
        {
            string state = "Fail...";
            try
            {
                fz.Password = ZipPWD;
                fz.ExtractZip(ZipPath, DirPath, null);

                state = "Success !";
            }
            catch (Exception ex)
            {
                state += "," + ex.Message;
            }
            return state;
        }

















        /// <summary>
        /// 单文件压缩（生成的压缩包和第三方的解压软件兼容）
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        public static string CompressSingle(string sourceFilePath)
        {
            string zipFileName = sourceFilePath + ".gz";
            using (FileStream sourceFileStream = new FileInfo(sourceFilePath).OpenRead())
            {
                using (FileStream zipFileStream = File.Create(zipFileName))
                {
                    using (GZipStream zipStream = new GZipStream(zipFileStream, CompressionMode.Compress))
                    {
                        sourceFileStream.CopyTo(zipStream);
                    }
                }
            }
            return zipFileName;
        }
        /// <summary>
        /// 自定义多文件压缩（生成的压缩包和第三方的压缩文件解压不兼容）
        /// </summary>
        /// <param name="sourceFileList">文件列表</param>
        /// <param name="saveFullPath">压缩包全路径</param>
        public static void CompressMulti(string[] sourceFileList, string saveFullPath)
        {
            MemoryStream ms = new MemoryStream();
            foreach (string filePath in sourceFileList)
            {
                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);
                    byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
                    byte[] sizeBytes = BitConverter.GetBytes(fileNameBytes.Length);
                    ms.Write(sizeBytes, 0, sizeBytes.Length);
                    ms.Write(fileNameBytes, 0, fileNameBytes.Length);
                    byte[] fileContentBytes = System.IO.File.ReadAllBytes(filePath);
                    ms.Write(BitConverter.GetBytes(fileContentBytes.Length), 0, 4);
                    ms.Write(fileContentBytes, 0, fileContentBytes.Length);
                }
            }
            ms.Flush();
            ms.Position = 0;
            using (FileStream zipFileStream = File.Create(saveFullPath))
            {
                using (GZipStream zipStream = new GZipStream(zipFileStream, CompressionMode.Compress))
                {
                    ms.Position = 0;
                    ms.CopyTo(zipStream);
                }
            }
            ms.Close();
        }

        /// <summary>
        /// 多文件压缩解压
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="targetPath">解压目录</param>
        public static void DeCompressMulti(string zipPath, string targetPath)
        {
            byte[] fileSize = new byte[4];
            if (File.Exists(zipPath))
            {
                using (FileStream fStream = File.Open(zipPath, FileMode.Open))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (GZipStream zipStream = new GZipStream(fStream, CompressionMode.Decompress))
                        {
                            zipStream.CopyTo(ms);
                        }
                        ms.Position = 0;
                        while (ms.Position != ms.Length)
                        {
                            ms.Read(fileSize, 0, fileSize.Length);
                            int fileNameLength = BitConverter.ToInt32(fileSize, 0);
                            byte[] fileNameBytes = new byte[fileNameLength];
                            ms.Read(fileNameBytes, 0, fileNameBytes.Length);
                            string fileName = System.Text.Encoding.UTF8.GetString(fileNameBytes);
                            string fileFulleName = targetPath + fileName;
                            ms.Read(fileSize, 0, 4);
                            int fileContentLength = BitConverter.ToInt32(fileSize, 0);
                            byte[] fileContentBytes = new byte[fileContentLength];
                            ms.Read(fileContentBytes, 0, fileContentBytes.Length);
                            using (FileStream childFileStream = File.Create(fileFulleName))
                            {
                                childFileStream.Write(fileContentBytes, 0, fileContentBytes.Length);
                            }
                        }
                    }
                }
            }
        }



    }

    public class QrCodeUtil
    {

        /*qrcode 使用方法
        1.引用【右击】->管理NuGet程序包->搜索‘qrcode’ ->安装 QrCode.net
        2.引入下列命名空间
            using Gma.QrCodeNet.Encoding;
            using Gma.QrCodeNet.Encoding.Windows.Render;
            using System.Drawing.Imaging;
            using System.IO;
            using System.Drawing;

        */

        //目前只支持英文  content-需生成二位的字符串    fileName-文件绝对位置及文件名(png文件)
        public static string CreateQrCode(string url, string text, string mkdir = @"QrCode\Images")
        {
            string directoryName = HttpContext.Current.Server.MapPath("~/" + mkdir); // 文件路径
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            string fileName = "/" + text + "_" + System.Guid.NewGuid().ToString("N") + ".png";
            string filePath = directoryName + fileName;
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(url);
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(20, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                render.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
                stream.Close();
            }
            AddText(text, filePath);
            fileName = mkdir + fileName;
            return fileName;
        }

        private static void AddText(string text, string filePath)
        {
            Font font = new Font("GB2312", 14, FontStyle.Regular);//设置字体，大小，粗细
            SolidBrush sbrush = new SolidBrush(Color.Black);//设置颜色
            try
            {
                Bitmap im = new Bitmap(filePath);
                Bitmap bmp = new Bitmap(300, 340); //定义图片大小
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                g.DrawString(text, font, sbrush, new PointF((Int32)(bmp.Width - text.Length * 20) / 2, bmp.Height - 40));
                // 合并位图
                g.DrawImage(im, new Rectangle(0, 0, 300, 300));
                im.Dispose();
                bmp.Save(filePath, ImageFormat.Png);
                g.Dispose();
                bmp.Dispose();
            }
            catch
            {

            }

        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var filePath = QrCodeUtil.CreateQrCode("http://baidu.com", "你好");

            RARComman.FileToZip(@"D:\个人\Demo\Project\mv_my_ms\QRCode\rar.zip", @"D:\个人\Demo\Project\mv_my_ms\QRCode\1.png", null);






            List<string> strList = new List<string>() { @"D:\个人\Demo\Project\mv_my_ms\QRCode\1.png", @"D:\个人\Demo\Project\mv_my_ms\QRCode\2.png", @"D:\个人\Demo\Project\mv_my_ms\QRCode\3.png" };
            RARComman.CompressMulti(strList.ToArray(), @"D:\个人\Demo\Project\mv_my_ms\QRCode\rar1.zip");
            RARComman.DeCompressMulti(@"D:\个人\Demo\Project\mv_my_ms\QRCode\rar1.zip", @"D:\个人\Demo\Project\mv_my_ms\QRCode\unrar\");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        void G(int pagesize, int sleep, int count)
        {
            LY_DBEntities entities = new LY_DBEntities();
            List<mv_error_log> logList = new List<mv_error_log>();
            List<mv_error_log> resultLogList = entities.mv_error_log.Where(t => !t.isdispose).ToList();
            if (resultLogList.Count() == 0)
            {
                return;
            }
            else
            {
                count++;
                foreach (var item in resultLogList)
                {
                    item.isdispose = true;
                    try
                    {
                        int result = new LY_DBEntities().mv_mysql_data("demo_xkm", (int)item.pageindex, pagesize);
                    }
                    catch (Exception ex)
                    {
                        logList.Add(new mv_error_log { pageindex = (int)item.pageindex, error_msg = ex.Message + ex.GetBaseException(), count = count, isdispose = false, sleep = sleep });
                    }
                }
                entities.mv_error_log.AddRange(logList);
                entities.SaveChanges();

                Thread.Sleep(1000 * sleep);
                sleep *= 2;//1 2 4 6 9 13 11 22
                if (sleep == 8) sleep -= 5;
                if (sleep == 12) sleep -= 3;
                if (sleep == 18) sleep -= 5;
                if (sleep == 26) sleep -= 15;
                //如果大于15次 就结束
                if (count > 15) return;
                G(pagesize, sleep, count);
            }

        }

        public ActionResult MV_MY_MS()
        {
            var allcount = Convert.ToInt32(new LY_DBEntities().mv_mysql_data_count().FirstOrDefault());
            var pagesize = 20;
            var allpageindex = allcount % pagesize == 0 ? allcount / pagesize : (allcount / pagesize) + 1;
            int sleep = 1;
            int count = 0;

            Task.Factory.StartNew(() =>
            {
                List<mv_error_log> logList = new List<mv_error_log>();
                for (int i = 0; i < allpageindex; i++)
                {
                    try
                    {
                        int result = new LY_DBEntities().mv_mysql_data("demo_xkm_1", i, pagesize);
                    }
                    catch (Exception ex)
                    {
                        logList.Add(new mv_error_log { pageindex = i, error_msg = ex.Message, count = count, isdispose = false, sleep = sleep });
                    }
                }
                LY_DBEntities entities = new LY_DBEntities();
                entities.mv_error_log.AddRange(logList);
                entities.SaveChanges();
                G(pagesize, sleep, count);



            });
            return View(allcount);
        }
    }
}