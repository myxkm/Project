using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace API.Controllers
{

    public class FFMpeg
    {
        public static void WriteFile(string strPathFile, string strContent, bool boolNeedMapPath = true)
        {
            string strpath = "";

            if (boolNeedMapPath == false)
            {
                strpath = strPathFile;

            }
            else
            {
                strpath = System.Web.HttpContext.Current.Server.MapPath(strPathFile);
            }


            StreamWriter sr = null;
            //new StreamWriter(strpath);

            if (!File.Exists(strpath))
            {
                sr = File.CreateText(strpath);
            }
            else
            {
                File.Delete(strpath);
                sr = File.CreateText(strpath);
            }
            sr.Write(strContent);
            sr.Close();

        }

        private static string Execute(string command, int seconds)
        {
            string output = ""; //输出字符串  
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动 
                startInfo.RedirectStandardInput = false;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.CreateNoWindow = true;//不创建窗口  
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程  
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束  
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出  
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);//捕获异常，输出异常信息
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }

        public  bool CombileToMp3(List<string> MapPathlist, string outPathFileNormalName)
        {
            bool dddd = false;
            try
            {
                {
                    string strAllList = "";
                    foreach (var myfile in MapPathlist)
                    {
                        string writeLine = "file " + myfile + "\r\n";
                        strAllList += writeLine;
                    }
                    string strMp3List = @"C:\windows\temp\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + System.Guid.NewGuid().ToString("N") + ".txt";
                    WriteFile(strMp3List, strAllList, false);

                    string ffmpeg = System.Windows.Forms.Application.StartupPath + @"\ffmpeg.exe -f concat -safe 0 -i {0} -c copy {1}";
                    ffmpeg = string.Format(ffmpeg, strMp3List, outPathFileNormalName);

                    string sttr = (Execute(ffmpeg, 10));
                }

                dddd = true;
            }
            catch (Exception eeee)
            {
                int ddd = 0;
            }
            return dddd;

        }



        public void CombineMp4WithoutTxt(string StrMP4A, string StrMP4B, string StrOutMp4Path)
        {
            Process p = new Process();//建立外部调用线程
            p.StartInfo.FileName = System.Windows.Forms.Application.StartupPath + "\\ffmpeg.exe";//要调用外部程序的绝对路径
            //ffmpeg.exe - i "StrMP4A" - b:a 192k - acodec mp3 - ar 44100 - ac 2 "转换后的音频文件.mp3"
          
            string StrArg = "  -i concat:\"" + StrMP4A + "|" + StrMP4B + "\" -vcodec copy -acodec copy " + StrOutMp4Path + " ";
            p.StartInfo.Arguments = StrArg;
            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程(一定为FALSE,详细的请看MSDN)
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中(这个一定要注意,FFMPEG的所有输出信息,都为错误输出流,用StandardOutput是捕获不到任何消息的...这是我耗费了2个多月得出来的经验...mencoder就是用standardOutput来捕获的)
            p.StartInfo.CreateNoWindow = true;//不创建进程窗口
            p.ErrorDataReceived += new DataReceivedEventHandler(Output);//外部程序(这里是FFMPEG)输出流时候产生的事件,这里是把流的处理过程转移到下面的方法中,详细请查阅MSDN
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }
        private void Output(object sendProcess, DataReceivedEventArgs output)
        {
            if (!String.IsNullOrEmpty(output.Data))
            {
                //处理方法...
            }
        }
    }


    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Geti()
        {
            throw new Exception("1222222");

            return new string[] { "value1", "value2" };
        }

        public void Combine(string[] inputFiles, Stream output)
        {
            foreach (string file in inputFiles)
            {
                var a = HttpContext.Current.Server.MapPath(file);
                Mp3FileReader reader = new Mp3FileReader(a);
                if ((output.Position == 0) && (reader.Id3v2Tag != null))
                {
                    output.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                }
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                {
                    output.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
        }
        public string tLod(int id)
        {
            FileStream stream = null;
            try
            {
                var newMp3Path = "/MP3/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".mp3";
                string filePath = HttpContext.Current.Server.MapPath("~" + newMp3Path); // 文件路径
                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                List<string> list = new List<string>() { "~/mp2/2.mp3", "~/mp2/1.mp3" };



                stream = new FileStream((filePath), FileMode.Create);
                Combine(list.ToArray(), stream);

                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                stream.Close();
            }
            catch (Exception ex)
            {
                if (stream != null) stream.Close();
            }



            return "value";
        }

        // GET api/values/5
        public string Get(int id)
        {

            try
            {
                var newMp3Path = "/MP3/" + DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss") + ".mp3";
                string filePath = HttpContext.Current.Server.MapPath("~" + newMp3Path); // 文件路径
                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }


                List<string> list = new List<string>() { @"D:\个人\Demo\Project\API\MP2\2.mp3", @"D:\个人\Demo\Project\API\MP2\1.mp3" };

                new FFMpeg().CombileToMp3(list, filePath);



            }
            catch (Exception ex)
            {

            }



            return "value";
        }



        // POST api/values
        public void Post([FromBody]string value)
        {



        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
