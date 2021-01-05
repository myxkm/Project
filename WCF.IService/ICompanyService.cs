using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF.Model;

namespace WCF.IService
{

    [ServiceContract]
    public interface ICompanyService
    {
        [OperationContract]
        IList<Company> GetAllList();
        [OperationContract]
        IList<Company> GetList(string Name);
        [OperationContract]
        Company Add(Company model);
        [OperationContract]
        bool Delete(long Id);
    }
}
