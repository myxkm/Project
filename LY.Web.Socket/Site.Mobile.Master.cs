using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LY.Web.Socket
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie httpCookie = new HttpCookie("CurrentUser");
            httpCookie.Value = "1212";
            httpCookie.Expires = DateTime.Now.AddHours(1);
            httpCookie.Domain = "http://www.baidu.com";
            Response.Cookies.Add(httpCookie);

            var session = "122".ToString();


        }
        public override string ToString()
        {

            return "";
        }

    }
}