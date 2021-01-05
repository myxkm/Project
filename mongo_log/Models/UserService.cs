using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MongoDBUtils.Models
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository = new MongoRepository<User>();
        public void Add(User user)
        {
            _repository.Add(user);
        }

        public void Find(Expression<Func<User, bool>> expression = null)
        {
            _repository.Find(expression);
        }

        public void FindOne()
        {
            _repository.Find().FirstOrDefault();
        }

        public void Remove(User user)
        {
            _repository.Remove(user);
        }

        public void Update(User user, List<string> properts = null, bool replace = false)
        {
            _repository.Update(user, properts, replace);
        }
    }
}