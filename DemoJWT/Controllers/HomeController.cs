using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace a
{
    using Newtonsoft.Json.Linq;
    using System.Net.Http;
    using System.Web.Http;
    public class OAuthController : ApiController
    {

        CodeModel getCodeModel()
        {
            List<CodeModel> codeModels = new List<CodeModel>() { };
            codeModels.Add(new CodeModel() { code = "1", date = DateTime.Now.AddDays(1), data = "clientCode,clientSecret", redirectUri = "" });
            codeModels.Add(new CodeModel() { code = "2", date = DateTime.Now.AddDays(1), data = "clientCode,clientSecret", redirectUri = "" });
            codeModels.Add(new CodeModel() { code = "3", date = DateTime.Now.AddDays(1), data = "clientCode,clientSecret", redirectUri = "" });
            return codeModels.FirstOrDefault();

        }
        public class CodeModel
        {
            public string code { get; set; }
            public DateTime date { get; set; }
            public string data { get; set; }
            public string redirectUri { get; set; }


        }

        //secret是保存在服务器端的，
        //1jwt的签发生成也是在服务器端的，
        //2 secret就是用来进行jwt的签发和jwt的验证，所以，它就是你服务端的私钥，在任何场景都不应该流露出去。一旦客户端得知这个secret, 那就意味着客户端是可以自我签发jwt了。

        private string secret = "abcdefjhijkmhadhashdkaxiaomaPrinxiaomaPrincesscessshdahd";
        [HttpGet]
        public void Authorize(string clientCode, string clientSecret, string redirectUri, string scope)
        {
            //验证clientCode, string clientSecret 
            var code = clientCode + clientSecret;
            var dic = new Dictionary<string, string>();
            dic.Add("code", code);
            HttpClient httpClient = new HttpClient();
            httpClient.PostAsync(redirectUri, new FormUrlEncodedContent(dic));
        }
        [HttpPost]
        public dynamic Token([FromBody] JObject obj)
        {
            var objCode = obj["code"];
            var redirectUri = obj["redirectUri"];


            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
            //var payload = new Dictionary<string, object> { { "iss", " jwt签发者xkm" }, { "name", "MrBug" }, { "exp", secondsSinceEpoch + 100 }, { "jti", "luozhipeng" } };
            var payload = getCodeModel();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            return new { token };
        }
        [HttpGet]
        public string Decode()
        {
            var a = HttpContext.Current.Handler;
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiLkuLvpopgiLCJqdGkiOiI5OGVlZjE2MC02ODIwLTRlMGQtOWRiOS1iNzIyODcyMDIxNGQiLCJpYXQiOiIxNjA1NjcxNjQ5IiwiZGF0YSI6IntcImFjY291bnRcIjpcInVzZXJfeGttXCJ9IiwibmJmIjoxNjA1NjcxNjUyLCJleHAiOjE2MzcyMDc2NTEsImlzcyI6Imd1ZXRTZXJ2ZXIiLCJhdWQiOiJndWV0Q2xpZW50In0.7aQ_xoq5y2M6KDa8oxQH9XkQHSRqv17A0WXT9jvMZAU";
            string json = string.Empty;
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                json = decoder.Decode(token, secret, verify: true);
            }
            catch (TokenExpiredException e)
            {
                json = e.Message;
            }
            catch (SignatureVerificationException e)
            {
                json = e.Message;
            }
            return json;
        }

    }
}

namespace DemoJWT.Controllers
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Mvc;

    public class HomeController : Controller
    {

        //login   redirect_uri  换取code state  这个code 设置七分钟 并只可以使用一次 
        //通过code state 获取token  
        private string secret = "xkm.......";
        string redirectUri = "http://localhost:53397/home/RedirectUrl";
        public ActionResult Index()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.GetAsync("http://localhost:53397/api/oauth/Authorize?clientCode=clientCode&clientSecret=clientSecret&redirectUri=" + redirectUri + "&scope=");
            //var json = DecodeToken("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiIgand0562-5Y-R6ICFeGttIiwibmFtZSI6Ik1yQnVnIiwiZXhwIjoxNTYyODMyODA3LjAsImp0aSI6Imx1b3poaXBlbmcifQ.7oCGSLNsPd2_aSjT_70gZ85z6Pd_AZRdcHRgdYULGIQ");
            //ViewBag.json = json;
            return View();
        }
        [HttpPost]
        public void RedirectUrl(string code, string state)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("code", code);
            dic.Add("redirectUri", redirectUri);

            string token = "";
            using (HttpClient httpClient = new HttpClient())
            {
            var result = httpClient.PostAsync("http://localhost:53397/api/oauth/Token", new FormUrlEncodedContent(dic)).Result.Content.ReadAsStringAsync();
            var responseValue = result.Result;
            var dy = (dynamic)JsonConvert.DeserializeObject(responseValue);
             token = dy.token;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = httpClient.GetAsync("http://localhost:53397/api/oauth/Decode");
            }
        }



        
      
    }


    public class CustomJsonSerializer : IJsonSerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);

        }
    }
}
