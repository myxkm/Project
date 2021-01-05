using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using coreapi.LogApiHandler;
using coreapi.Models;
using JWT;
using JWT.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace coreapi.Controllers
{

    /// <summary>
    /// 授权中心
    /// </summary>
    [ApiController]  
    [Route("[controller]")]
    public class OAuthController : CustomBaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OAuthController> _logger;
        static readonly Logger logger = LogApiHandler.Logger.GetLogger("logSource");
        public OAuthController(IConfiguration configuration, ILogger<OAuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// <![CDATA[获取访问令牌,签发中心，签发令牌]]>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public RestfulData<TokenObj> Token(string user = "user_xkm", string password = "")
        {
            logger.Error("111111111111");

            _logger.LogInformation($"user:{user}  password:{password}");
            var JwtSettings = _configuration.GetSection("JwtSettings");
            var result = new RestfulData<TokenObj>();
            try
            {
                if (string.IsNullOrEmpty(user)) throw new ArgumentNullException("user", "用户名不能为空！");
                if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password", "密码不能为空！");
                if (!password.Equals("1#")) throw new ArgumentNullException("password", "密码不正确！");

                //验证数据库用户名和密码
                //var userInfo = await _UserService.CheckUserAndPassword(user,  password);

                var Audience = JwtSettings["Audience"];
                var Issuer = JwtSettings["Issuer"];



                var claims = new Claim[]
                {
                new Claim(JwtRegisteredClaimNames.Sub,"主题"),//Subject,
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//JWT ID,JWT的唯一标识
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                  //Issued At，JWT颁发的时间，采用标准unix时间，用于验证过期
                    new Claim("data",Newtonsoft.Json.JsonConvert.SerializeObject(new {  account=user})),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokenHandler = new JwtSecurityTokenHandler();
                var expires = DateTime.Now.AddYears(1);//token 有效期 为一年的
                JwtSecurityToken jwt = new JwtSecurityToken(
                           issuer: Issuer,// 签发者
                           audience: Audience,// 接收者
                           claims: claims,
                           notBefore: DateTime.Now,
                           expires: expires,
                           signingCredentials: creds);
                //生成Token
                string token = tokenHandler.WriteToken(jwt);
                result.Code = 200;
                result.Data = new TokenObj() { Token = token, Expires = new DateTimeOffset(expires).ToUnixTimeSeconds() };
                result.Message = "授权成功！";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Code = 400;
                _logger.LogError("获取访问令牌时发生错误！" + ex.Message, ex);
                return result;
            }
        }


        [HttpGet]
        public string Decode()
        {
            var JwtSettings = _configuration.GetSection("JwtSettings");
            var SecurityKey = JwtSettings["SecurityKey"];
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiLkuLvpopgiLCJqdGkiOiI5OGVlZjE2MC02ODIwLTRlMGQtOWRiOS1iNzIyODcyMDIxNGQiLCJpYXQiOiIxNjA1NjcxNjQ5IiwiZGF0YSI6IntcImFjY291bnRcIjpcInVzZXJfeGttXCJ9IiwibmJmIjoxNjA1NjcxNjUyLCJleHAiOjE2MzcyMDc2NTEsImlzcyI6Imd1ZXRTZXJ2ZXIiLCJhdWQiOiJndWV0Q2xpZW50In0.7aQ_xoq5y2M6KDa8oxQH9XkQHSRqv17A0WXT9jvMZAU";
            string json = string.Empty;
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                json = decoder.Decode(token, SecurityKey, verify: true);
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
