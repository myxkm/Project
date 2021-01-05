using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBUtils.Models
{
    public interface ICore
    {
        string Id { set; get; }
    }


    /// <summary>
    /// 实体
    /// </summary>
    public abstract class Core : ICore
    {
        public string Id { set; get; } = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// 用户
    /// </summary>
    public class User : Core, IAggregateRoot
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string Cardcertificate { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { set; get; } = Gender.Boy;

        /// <summary>
        /// 用户的房屋信息
        /// </summary>
        public List<House> Houses { set; get; } = new List<House>();
    }


    public class House : Core
    {
        public string Adress { set; get; }
    }

    public enum Gender
    {
        Boy = 0,
        Gril = 1
    }
  
 

}