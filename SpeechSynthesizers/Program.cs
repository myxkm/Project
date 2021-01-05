using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeechSynthesizers
{
    class Program
    {
        static void Main(string[] args)
        {




            //string prms = "王东宇" + " " + "王东宇 " + " " + "王东宇" + " " + "王东宇" + " " + "王东宇" + " " + "王东宇" + " " + "王东宇" + " " + "王东宇";
            //string msgboxPath = Path.Combine(@"D:\个人\Demo\Project\SpeechSynthesizers\bin\Debug", "SpeechSynthesizers.exe");
            ////var Processs =new System.Diagnostics.Process();
            ////Processs.StartInfo.Verb = "Open";
            ////Processs.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            ////Processs.Start();
            ////System.Diagnostics.Process.
            ////System.Diagnostics.Process.Start(msgboxPath, prms);
            //Process pr = new Process();

            //pr.StartInfo.FileName = string.Format(msgboxPath);
            //pr.StartInfo.Verb = "Open";
            //pr.StartInfo.Arguments = prms;
            //pr.StartInfo.WindowStyle = ProcessWindowStyle.;

            //pr.Start();



            var a = string.Empty;

            foreach (var item in args)
            {
                a += item;
            }
         

            SpeechSynthesizer voice = new SpeechSynthesizer
            {
                Rate = 1, //设置语速,[-10,10]
                Volume = 100 //设置音量,[0,100]

            };   //创建语音实例
                 //voice.SpeakAsync("Hellow Word");  //播放指定的字符串,这是异步朗读
            //voice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Child,2,CultureInfo.CurrentCulture);

              a = File.ReadAllText("文本.txt");


            voice.SpeakProgress += Voice_SpeakProgress;


            //下面的代码为一些SpeechSynthesizer的属性，看实际情况是否需要使用
            //voice.Dispose();  //释放所有语音资源
            voice.SpeakAsyncCancelAll();  //取消朗读
            voice.Speak(a);  //同步朗读


            voice.Pause();  //暂停朗读
            voice.Resume(); //继续朗读
            Console.Read();
        }

        private static void Voice_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            var a = File.ReadAllText("文本.txt");

            //Console.WriteLine(e.CharacterPosition + "___" + e.CharacterCount);

            Console.Write(a.Substring(e.CharacterPosition, e.CharacterCount));
        }
    }
}
