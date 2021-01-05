using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemoLong.MongoDB;
using CoreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoCore.Models;
using MongoDB.Driver;

namespace CoreModel
{
    public class ApiMsSqlDBContent : DbContext
    {
        public ApiMsSqlDBContent(DbContextOptions<ApiMsSqlDBContent> options)
            : base(options)
        {
        }
        public DbSet<Z_Short> Z_Short { get; set; }
    }
    public class ApiMySqlDBContent : DbContext
    {
        public ApiMySqlDBContent(DbContextOptions<ApiMySqlDBContent> options)
            : base(options)
        {
        }
        public DbSet<Z_Short> Z_Short { get; set; }
    }


    public class Z_Short
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

namespace AspNetCoreDemoLong.Controllers
{


    public class LongController : Controller
    {
        private readonly ApiMsSqlDBContent _dbMsContext;
        private readonly ApiMySqlDBContent _dbMyContext;

        public LongController(ApiMsSqlDBContent dbMsContext, ApiMySqlDBContent dbMyContext)
        {

            _dbMsContext = dbMsContext;
            _dbMyContext = dbMyContext;
        }

        public List<Z_Short> ShortList()
        {
            return _dbMsContext.Z_Short.ToList();
        }
        private Z_Short AddShort(string Url)
        {
            var user = new Z_Short { Url = Url, CreateDate = DateTime.Now };
            _dbMsContext.Z_Short.Add(user);
            _dbMyContext.Z_Short.Add(user);
            _dbMsContext.SaveChanges();
            _dbMyContext.SaveChanges();

            return user;
        }
        private Z_Short InfoShort(long Id)
        {
            return _dbMsContext.Z_Short.FirstOrDefault(testc => testc.Id == Id);
        }
        private Z_Short InfoShort(string Url)
        {
            return _dbMsContext.Z_Short.FirstOrDefault(testc => testc.Url == Url);
        }
        private readonly MongoContext _context = new MongoContext("admins", "mongodb://root:root@127.0.0.1:27017");
        [HttpGet]
        public void Index(string Id)
        {
            var _collection = _context.GetMongoTableNameCollection<Dictionary<string, object>>("users");
            _collection.InsertOne(new Dictionary<string, object> { { "username","xkm11"}  });

            var user = InfoShort((long)Converter.To10(Id));
            Response.Redirect(user?.Url);
        }
        [HttpGet]
        public dynamic Short(string Url)
        {
            //Url = System.Web.HttpUtility.UrlDecode(Url);
            var user = InfoShort(Url);
            if (user == null) user = AddShort(Url);
            return Request.Host.Value + "/" + Converter.To62(user.Id);
            return Url;
        }
    }

    public class Converter
    {


        private static readonly string    keys = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly int  exponent = keys.Length;
        public static string To62(decimal value)
        {
            string result = string.Empty;
            do
            {
                decimal index = value % exponent;
                result = keys[(int)index] + result;
                value = (value - index) / exponent;
            }
            while (value > 0);

            return result;
        }
        public static decimal To10(string value)
        {
            decimal result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                int x = value.Length - i - 1;
                result += keys.IndexOf(value[i]) * Pow(exponent, x);// Math.Pow(exponent, x);
            }
            return result;
        }

        /// <summary>
        /// 一个数据的N次方
        /// </summary>
        /// <param name="baseNo"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        private static decimal Pow(decimal baseNo, decimal x)
        {
            decimal value = 1;////1 will be the result for any number's power 0.任何数的0次方，结果都等于1
            while (x > 0)
            {
                value *= baseNo;
                x--;
            }
            return value;
        }

    }
}