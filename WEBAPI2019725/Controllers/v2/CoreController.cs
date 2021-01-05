using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WEBAPI2019725.Controllers.v2
{
    public class CoreController : ApiController
    {
        public string GetIndex()
        {
            return "这是v2版本的Index";
        }
    }
}
