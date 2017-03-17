using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarDealerApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //     name: "Cars with list of parts",
            //     url: "cars/{id}/parts/",
            //     defaults: new { controller = "Cars", action = "About" },
            //     constraints: new { id = @"\d+" }
            //     );


            //routes.MapRoute(
            //     name: "Supliers Filtered",
            //     url: "suppliers/{type}/",
            //     defaults: new { controller = "Suppliers", action = "All" });

            //routes.MapRoute(
            //     name: "Cars by make",
            //     url: "cars/{make}/",
            //     defaults: new { controller = "Cars", action = "All" });

            //routes.MapRoute(
            // name: "Customers All ",
            // url: "Customers/all/{order}",
            // defaults: new { controller = "Customers", action = "All", order = "ascending" },
            // constraints: new { order = @"ascending|descending" }
            // );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
