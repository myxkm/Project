using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCF.ICallback;
using WCF.IService;
using WCF.Model;

namespace WCF.Service
{
    public class DepartmentService : IDepartmentService
    {
        public Department Add(Department model)
        {
            return model;
        }

        public bool Delete(long Id)
        {
            return Id > 0;
        }
        public IList<Department> GetAllList()
        {
            return new List<Department> { new Department { CreateTime = DateTime.Now, DepartmentName = "xkm", Id = 1 } };
        }
        public IList<Department> GetList(string Name)
        {
            return new List<Department> { new Department { CreateTime = DateTime.Now, DepartmentName = "xkm", Id = 1, } };
        }
        public void Plus(long m, long n)
        {
            var result = m + n;
            Thread.Sleep(2000);
            ICallbackDepartmentService callback = OperationContext.Current.GetCallbackChannel<ICallbackDepartmentService>();
            callback.Result(m, n, result);
        }
    }
}
