using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBUtils.Models
{
    public class BasicContext : MongoDBContext
    {
        public BasicContext() : base("BasicData") { }
        public BasicContext(string database) : base(database) { }
        public IMongoCollection<User> Users { get => DbSet<User>(); }
    }
}