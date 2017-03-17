using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    public class SalesController : Controller
    {
        private SalesService service;

        public SalesController()
        {
            this.service = new SalesService();
        }
        
        // GET: Sales
        [Route("Sales")]
        public ActionResult All()
        {
            IEnumerable<SaleVm> sales = this.service.GetAllSales();
            return View(sales);
        }

        [Route("Sales/{id}")]
        public ActionResult Detail(int id)
        {
            SaleVm sales = this.service.GetSaleById(id);
            return View(sales);
        }


        [Route("Sales/discounted")]
        [Route("Sales/discounted/{percent}/")]
        public ActionResult All(double? percent)
        {
            IEnumerable<SaleVm> sales = this.service.GetSaleByDiscount(percent);
            return View(sales);
        }

    }
}
