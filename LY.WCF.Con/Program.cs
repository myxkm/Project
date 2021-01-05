using LY.Framework.Email;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LY.WCF.Con
{
    [DataContract]
    public class StudentInfo
    {
        int studentID;

        string lastName;

        string firstName;

        [DataMember]
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        string GetStudentFullName(int studentId);

        [OperationContract]
        IEnumerable<StudentInfo> GetStudentInfo(int studentId);
    }


    public class StudentService : IStudentService
    {
        List<StudentInfo> list = new List<StudentInfo>();

        public StudentService()
        {
            list.Add(new StudentInfo { StudentID = 10010, FirstName = "Shi", LastName = "Chaoyang" });
            list.Add(new StudentInfo { StudentID = 10011, FirstName = "Liu", LastName = "Jieus" });
            list.Add(new StudentInfo { StudentID = 10012, FirstName = "Cheung", LastName = "Vincent" });
            list.Add(new StudentInfo { StudentID = 10013, FirstName = "Yang", LastName = "KaiVen" });
        }

        public string GetStudentFullName(int studentId)
        {
            IEnumerable<string> Student = from p in list
                                          where p.StudentID == studentId
                                          select p.FirstName + " " + p.LastName;
            return Student.Count() != 0 ? Student.First() : string.Empty;
        }

        public IEnumerable<StudentInfo> GetStudentInfo(int studentId)
        {
            IEnumerable<StudentInfo> Student = from p in list
                                               where p.StudentID == studentId
                                               select p;
            return Student;
        }
    }
    public class EricSimpleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello Eric, Job executed.");
            return Task.CompletedTask;
        }
    }
    public static class AccessToken

    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "nDHvTOVcKWDFvjCmUskIVKGB";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = "ol3I1AqEcn6kuLhIuD8QLZ8PQSMXGM9M";

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            //AccessToken.getAccessToken();

            // 设置APPID/AK/SK
            var APP_ID = "15150853";
            var API_KEY = "nDHvTOVcKWDFvjCmUskIVKGB";
            var SECRET_KEY = "ol3I1AqEcn6kuLhIuD8QLZ8PQSMXGM9M";
            var client = new Baidu.Aip.Speech.Asr(API_KEY, SECRET_KEY);
            var data = File.ReadAllBytes("16k.pcm");
            client.Timeout = 120000; // 若语音较长，建议设置更大的超时时间. ms
            var result = client.Recognize(data, "pcm", 16000);

            var resultO = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            var a = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(resultO);
            Console.Write(result);

          
            {
                //QQ邮件发送
                MailSmtp ms = new MailSmtp("smtp.qq.com", "1477165313", "nwmklrnfduhwfhgh", 25);
                ms.SetCC("1477165313@qq.com");//抄送可以多个
                ms.SetBC("480672885@qq.com");//暗送可以多个
                ms.SetIsHtml(true);//默认:true
                ms.SetEncoding(System.Text.Encoding.UTF8);//设置格式 默认utf-8
                ms.SetIsSSL(true);//是否ssl加密 默认为false
                //bool isSuccess = ms.Send("1477165313@qq.com", "谢昆明", "1477165313@qq.com", "哈哈", "哈哈", new string[] { });
            }
            {
                //企业邮件发送
                MailSmtp ms = new MailSmtp("mail.yto.net.cn", "otp_opmsystem@yto.net.cn", "YTO@2018", 25);
                bool isSuccess = ms.Send("otp_opmsystem@yto.net.cn", "谢昆明", "1477165313@qq.com", "哈哈", "哈哈", new string[] { });
            }
            Console.ReadKey();
        }


    }
  
}
