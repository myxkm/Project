﻿using System.Web;
using System.Web.Mvc;

namespace SQLSugar_X
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
