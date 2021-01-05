using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.ICallback;
using WCF.IService;
using WCF.Model;
using System.ServiceModel;
using System.Threading;

namespace WCF.Service
{
    public class ProductService : IProductService
    {
        public Product Add(Product model)
        {
            return model;
        }

        public IList<Product> GetAllList()
        {
            return new List<Product> { new Product { Id = 1 } };
        }

        public void Plus(long m, long n)
        {
            var result = m + n;
            Thread.Sleep(2000);
            ICallbackProductService callback = OperationContext.Current.GetCallbackChannel<ICallbackProductService>();
            callback.Result(m, n, result);
        }

    }
}
