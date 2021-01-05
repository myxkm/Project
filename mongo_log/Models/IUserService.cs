using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MongoDBUtils.Models
{
    public interface IUserService
    {
        void Add(User user);
        void Remove(User user);
        void FindOne();
        void Update(User user, List<string> properts = null, bool replace = false);
        void Find(Expression<Func<User, bool>> expression = null);
    }
}