using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
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

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance")] AddCarBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddCar(bind);
                return this.RedirectToAction("All", "Cars");
            }
            return this.View(this.service.GetAddCarVm(bind));

        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return View();
        }

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
