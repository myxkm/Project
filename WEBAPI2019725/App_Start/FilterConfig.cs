﻿using System.Web;
using System.Web.Mvc;

namespace WEBAPI2019725
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}