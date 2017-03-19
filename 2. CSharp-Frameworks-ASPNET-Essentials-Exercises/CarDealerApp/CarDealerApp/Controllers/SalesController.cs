using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Models.BindingModels.Sales;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Models.ViewModels.Sales;
using CarDealer.Services;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("sales")]
    public class SalesController : Controller
    {
        private SalesService service;

        public SalesController()
        {
            this.service = new SalesService();
        }
        
        // GET: Sales
        [Route]
        public ActionResult All()
        {
            IEnumerable<SaleVm> sales = this.service.GetAllSales();
            return View(sales);
        }

        [Route("{id}")]
        public ActionResult Detail(int id)
        {
            SaleVm sales = this.service.GetSaleById(id);
            return View(sales);
        }



        [Route("discounted/{percent?}/")]
        public ActionResult All(double? percent)
        {
            IEnumerable<SaleVm> sales = this.service.GetSaleByDiscount(percent);
            return View(sales);
        }

        [HttpGet]
        [Route("add/")]
        public ActionResult Add()
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------

            AddSaleVm vm = this.service.GetSalesVm();
            return View(vm);
        }

        [HttpPost]
        [Route("add/")]
        public ActionResult Add([Bind] AddSalesBm bind)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------

            if (ModelState.IsValid)
            {
                AddSaleConfirmationVm confirmVm = this.service.GetAddSaleConfirmationVm(bind);
                return this.RedirectToAction("AddConfirmation", confirmVm);
            }

            AddSaleVm addSaleVm = this.service.GetSalesVm();
            return View(addSaleVm);
        }

        [HttpGet]
        [Route("AddConfirmation")]
        public ActionResult AddConfirmation(AddSaleConfirmationVm vm)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------

            return this.View(vm);
        }

        [HttpPost]
        [Route("AddConfirmation")]
        public ActionResult AddConfirmation([Bind] AddSalesBm bind)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------

            if (ModelState.IsValid)
            {
                User loggedInUser = AuthenticationManager.GetAuthenticatedUser(httpCookie.Value);
                this.service.AddSale(bind, loggedInUser.Id);
                return this.RedirectToAction("All");
            }

            AddSaleConfirmationVm vm = this.service.GetAddSaleConfirmationVm(bind);
            return this.View(vm);
        }
    }
}
