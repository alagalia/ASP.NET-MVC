using System.Collections.Generic;
using System.Web.Mvc;
using EventApp.Services;
using EventsApp.Models.ViewModels.Event;

namespace EventsApp.Controllers
{
    public class HomeController : Controller
    {
        private HomeService service;

        public HomeController()
        {
            this.service = new HomeService();
        }

        public ActionResult Index()
        {
            IEnumerable<EventBriefVm> vms = this.service.Get6RecentlyEventsBriefVms();
            return View(vms);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Events()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}