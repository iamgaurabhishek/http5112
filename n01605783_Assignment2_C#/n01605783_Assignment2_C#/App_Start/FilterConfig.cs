﻿using System.Web;
using System.Web.Mvc;

namespace n01605783_Assignment2_C_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
