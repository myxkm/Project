using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF.Model;
using WCF.ICallback;

namespace WCF.IService
{
    [ServiceContract(CallbackContract = typeof(ICallbackUserService))]
    public interface IUserService
    {
        [OperationContract(IsOneWay = true)]
        void Add(User user);

        [OperationContract]
        IEnumerable<User> GetAllList();
    }
}
