using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealerApp.Filters;

namespace CarDealerApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult About()
        {
            var ctx = new CarDealer.Data.CarDealerContext();
            ViewBag.Message = "Your application description page." + ctx.Cars.Count();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //testing exception--------------
        [HttpGet]
        [Route("ex")]
        [HandleError(ExceptionType = typeof(Exception), View = "ArgumentException")]
        public ActionResult Ex()
        {
            throw new Exception("Some message for exeption");
        }
    }
}