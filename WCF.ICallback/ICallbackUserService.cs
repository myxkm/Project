using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ICallback
{
    public interface ICallbackUserService
    {
        [OperationContract(IsOneWay =true)]
        void Plus(long m, long n, long r);
    }
}
