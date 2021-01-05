using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MongoCore.Models

{


    public class School
    {
        public School(string schoolName, string years)
        {

            SchoolName = schoolName;
            Years = years;
        }
        public string SchoolName { get; set; }
        public string Years { get; set; }
    }


    public class Province
    {
        //[BsonId]
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public IList<School> SchoolName { get; set; }

    }

}

namespace AspNetCoreDemoLong.MongoDB
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database = null;
        public MongoContext(string DbName, string connectionString = "mongodb://127.0.0.1:27017")
        {
            var client = new MongoClient(connectionString);
            if (client != null)
                _database = client.GetDatabase(DbName);
        }
        public IMongoCollection<T> GetMongoTableNameCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
        public IMongoCollection<T> GetMongoTableNameCollection<T>(string tableName)
        {
            return _database.GetCollection<T>(tableName);
        }

    }
}
