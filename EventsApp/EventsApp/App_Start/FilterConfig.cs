﻿using System;
using System.Web.Mvc;

namespace EventsApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(Exception), 
                View = "Error"
            });
        }
    }
}
