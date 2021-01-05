using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace WcfService1
{
    public class CallbackDepartment : CallbackDepartmentService.IDepartmentServiceCallback
    {
        public void Result(long m, long n, long r)
        {
            throw new NotImplementedException();
        }
    }

    public class CallbackProduct : CallbackProductService.IProductServiceCallback
    {
        public void Result(long m, long n, long r)
        {
            throw new NotImplementedException();
        }
    }


    public class CallbackUser : CallbackUserService.IUserServiceCallback
    {
        public void Plus(long m, long n, long r)
        {
            throw new NotImplementedException();
        }
    }
}