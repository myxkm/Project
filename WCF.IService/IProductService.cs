using System.Collections.Generic;
using System.ServiceModel;
using WCF.ICallback;
using WCF.Model;

namespace WCF.IService
{
    [ServiceContract(CallbackContract = typeof(ICallbackProductService))]
    public interface IProductService
    {
        [OperationContract]
        Product Add(Product model);
        [OperationContract]
        IList<Product> GetAllList();

        [OperationContract(IsOneWay = true)]
        void Plus(long m,long n);
    }
}
