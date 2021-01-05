using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MongoDBUtils.Models
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        void Add(T entity);
        void Update(T entity, List<string> properts = null, bool replace = false);
        void Remove(T entity);

        IFindFluent<T, T> Find(Expression<Func<T, bool>> expression = null);
    }
}