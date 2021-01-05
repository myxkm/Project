using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBUtils.Models
{
    public interface IMongoDBContext
    {
        /// <summary>
        /// 具体的表连接器
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        IMongoCollection<K> DbSet<K>() where K : IAggregateRoot;


    }
    public class MongoDBContext : IMongoDBContext
    {
        private static readonly string conn = System.Configuration.ConfigurationManager.AppSettings["MongoString"];

        public MongoDBContext(string database)
        {
            _database = database;
            Client = new MongoClient(conn);
        }

        private readonly string _database = "BasicData";
        private readonly MongoClient Client = null;
        private IMongoDatabase Database { get => Client.GetDatabase(_database); }
        public IMongoCollection<K> DbSet<K>() where K : IAggregateRoot => Database.GetCollection<K>(typeof(K).Name);

    }

}