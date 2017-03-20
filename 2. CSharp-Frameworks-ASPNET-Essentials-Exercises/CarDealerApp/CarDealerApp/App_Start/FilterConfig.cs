using System;
using System.Web.Mvc;

namespace CarDealerApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new OutputCacheAttribute()
            {
                Duration = 10
            });
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(Exception),
                View = "ArgumentException"
            });
        }
    }
}
