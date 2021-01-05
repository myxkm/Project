using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF.ICallback;
using WCF.IService;
using WCF.Model;

namespace WCF.Service
{
    public class UserService : IUserService
    {
        public void Add(User user)
        {
            ICallbackUserService callback = OperationContext.Current.GetCallbackChannel<ICallbackUserService>();
            callback.Plus(user.Id, user.Id, user.Id + user.Id);
        }
        public IEnumerable<User> GetAllList()
        {
            var list = new List<User>() { new User() { Id = 1 }, new User() { Id = 2 } };
            foreach (var item in list)
                yield return item;
        }
    }
}
