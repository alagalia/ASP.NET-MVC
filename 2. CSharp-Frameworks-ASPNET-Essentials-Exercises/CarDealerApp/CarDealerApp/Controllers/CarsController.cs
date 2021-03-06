﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;
using CarDealerApp.Filters;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    
    [RoutePrefix("cars")]
    [Log]
    public class CarsController : Controller
    {
        private CarService service;

        public CarsController()
        {
            this.service = new CarService();
        }
        
        [HttpPost]
        [Route("add")]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "ArgumentException")]
        public ActionResult AddCarWithParts([Bind(Include = "Make, Model, TravelledDistance")] AddCarBm bind)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------


            //exeption testing-------------------------------
            if (bind.TravelledDistance < 0)
            {
                throw new ArgumentOutOfRangeException("bind", bind.TravelledDistance, "The car cannot have travelled distance with negative value!");
            }
            //end exeption testing -------------------------


            if (this.ModelState.IsValid)
            {
                User loggedInUser = AuthenticationManager.GetAuthenticatedUser(httpCookie.Value);
                this.service.AddCar(bind, loggedInUser.Id);
                return this.RedirectToAction("All", "Cars");
            }
            return this.View(this.service.GetAddCarWithPartsVm(bind));

        }

        [HttpGet]
        [Route("add")]
        
        public ActionResult AddCarWithParts()
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------
            return View(this.service.GetAddCarWithPartsVm(new AddCarBm()));
        }


        //[HttpPost]
        //[Route("add")]
        //public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance")] AddCarBm bind)
        //{
        //    if (this.ModelState.IsValid)
        //    {
        //        this.service.AddCar(bind);
        //        return this.RedirectToAction("All", "Cars");
        //    }
        //    return this.View(this.service.GetAddCarVm(bind));

        //}

        //[HttpGet]
        //[Route("add")]
        //public ActionResult Add()
        //{
        //    return View();
        //}

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
