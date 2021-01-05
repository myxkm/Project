using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WEBAPI2019725.Controllers.v1
{
    public class CoreController : ApiController
    {
        public string GetIndex()
        {

            {
                OneService.Service1Client client = null;
                try
                {
                    client = new OneService.Service1Client();
                    client = new OneService.Service1Client();
                    client.GetData(1);


                    client.GetDataUsingDataContract(new OneService.CompositeType { });


                    client.Close();

                }
                catch (Exception)
                {
                    if (client != null) client.Abort();
                    throw;
                }


            }

            {

                TcpService.CompanyServiceClient client = null;
                try
                {
                    client = new TcpService.CompanyServiceClient();
                    var allList = client.GetAllList();
                    foreach (var item in allList)
                    {

                    }
                    client.Close();
                }
                catch (Exception)
                {
                    if (client != null) client.Abort();
                    throw;
                }


            }


            return "这是v1版本的Index";
        }
    }
}
