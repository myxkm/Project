using System.Collections.Generic;
using System.ServiceModel;
using WCF.Model;
using WCF.ICallback;

namespace WCF.IService
{

    [ServiceContract(CallbackContract = typeof(ICallbackDepartmentService))]
    public interface IDepartmentService
    {
        [OperationContract]
        IList<Department> GetAllList();
        [OperationContract]
        IList<Department> GetList(string Name);
        [OperationContract]
        Department Add(Department model);
        [OperationContract]
        bool Delete(long Id);

        [OperationContract(IsOneWay = true)]
        void Plus(long m, long n);
    }
}
