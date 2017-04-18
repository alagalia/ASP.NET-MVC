using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using EventsApp.Models.ViewModels.Promoter;

namespace EventsApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<PromoterInfo, PromoterInfoVm>();

                expression.CreateMap<AddEventBm, Event>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image));

                expression.CreateMap<Event, EventAllVm>()
                .ForMember(dest =>dest.CategoryName, opt=>opt.MapFrom(src=>src.Category.Name))
                .ForMember(dest=>dest.ComentsCouner, opt=>opt.MapFrom(src=>src.Comments.Count));

                expression.CreateMap<Event, EventVm>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Owner.Id))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Id))
               .ForMember(dest => dest.CommentsCounter, opt => opt.MapFrom(src => src.Comments.Count));

                expression.CreateMap<Comment, CommentVm>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

                expression.CreateMap<IEnumerable<Event>, IEnumerable<EventBriefVm>>();
                expression.CreateMap<IEnumerable<Event>, IEnumerable<EventAllVm>>();
                expression.CreateMap<IEnumerable<Event>, IEnumerable<EventVm>>();
            });
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
