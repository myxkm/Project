using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Services;

namespace WcfService1
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            CallbackDepartmentService.DepartmentServiceClient client = null;
            try
            {
                InstanceContext context = new InstanceContext(new CallbackDepartment());
                client = new CallbackDepartmentService.DepartmentServiceClient(context);
                var list = client.GetAllList();
                client.Plus(1, 2);
                client.Close();

            }
            catch (Exception)
            {
                if (client != null) client.Abort();
            }






            return "Hello World";
        }

        [WebMethod]
        public string HelloWorld0()
        {
            CallbackProductService.ProductServiceClient client = null;

            try
            {
                InstanceContext context = new InstanceContext(new CallbackProduct());
                client = new CallbackProductService.ProductServiceClient(context);
                var allList = client.GetAllList();
                client.Add(new CallbackProductService.Product { });
                client.Plus(1, 2);
                client.Close();

            }
            catch (Exception)
            {
                if (client != null) client.Abort();
            }


            return "Hello World";
        }



        [WebMethod]
        public string HelloWorld1()
        {
            CallbackUserService.UserServiceClient client = null;
            try
            {
                InstanceContext context = new InstanceContext(new CallbackUser());

                client = new CallbackUserService.UserServiceClient(context);

                var list = client.GetAllList();
                client.Add(new CallbackUserService.User { Id=10 });
            }
            catch (Exception ex)
            {

                throw;
            }


            return "Hello World";
        }

    }
}
