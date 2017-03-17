using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("cars")]
    public class CarsController : Controller
    {
        private CarService service;

        public CarsController()
        {
            this.service = new CarService();
        }

        // GET: Cars
        [Route("{make?}")]
        public ActionResult All(string make)
        {
            IEnumerable<CarVm> viewModels = this.service.GetAllCars(make);
            return View(viewModels);
        }

        [Route("{id:int}/parts")]
        public ActionResult About(int id)
        {
            CarPartsVm viewModels = this.service.GetACarWithParts(id);
            return View(viewModels);
        }
    }
}
