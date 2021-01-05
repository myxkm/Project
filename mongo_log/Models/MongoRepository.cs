using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MongoDBUtils.Models
{
    public class MongoRepository<T> : Repository<T> where T : IAggregateRoot
    {
        public MongoRepository():base(new BasicContext()) { }
    }
}