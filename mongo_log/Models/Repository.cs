using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MongoDBUtils.Models
{
    public class Repository<T> : IRepository<T> where T : IAggregateRoot
    {
        private MongoDBContext _context = null;
        public Repository(MongoDBContext context) {
            _context = context;
        }
        public Repository() { }
        public void Add(T entity)
        {
            _context.DbSet<T>().Add(entity);
        }

        public IFindFluent<T, T> Find(Expression<Func<T, bool>> expression = null)
        {
           return  _context.DbSet<T>().Where(expression);
        }

        public void Remove(T entity)
        {
            _context.DbSet<T>().Remove(entity);
        }

        public void Update(T entity, List<string> properts = null, bool replace = false)
        {
            _context.DbSet<T>().Update(entity, properts,replace);
        }
    }
}