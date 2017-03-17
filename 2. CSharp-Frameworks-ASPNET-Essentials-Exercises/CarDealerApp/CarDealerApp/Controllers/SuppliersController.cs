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
    public class SuppliersController : Controller
    {
        private SuppliersService service;

        public SuppliersController()
        {
            this.service = new SuppliersService();
        }

        [Route("suppliers/{type?}")]
        public ActionResult All(string type)
        {
            IEnumerable<SupplierVm> viewModels = this.service.GetSuppliersFromDb(type);
            return View(viewModels);
        }

        
       
    }
}
