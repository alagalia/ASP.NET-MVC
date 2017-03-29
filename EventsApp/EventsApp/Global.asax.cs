using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;

namespace EventsApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<Event, EventVm>()
                .ForMember(dest=>dest.UserId, opt => opt.MapFrom(src=>src.Owner.Id))
                .ForMember(dest =>dest.Category, opt=>opt.MapFrom(src=>src.Category.Id));
            });
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
