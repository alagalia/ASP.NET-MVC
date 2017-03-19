using System.Collections.Generic;
using System.Web.Mvc;
using CarDealer.Models.BindingModels.Suppliers;
using CarDealer.Models.EntityModels;
using CarDealer.Models.ViewModels;
using CarDealer.Models.ViewModels.Sales;
using CarDealer.Models.ViewModels.Supplier;
using CarDealer.Services;
using CarDealerApp.Security;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("suppliers")]
    public class SuppliersController : Controller
    {
        private SuppliersService service;

        public SuppliersController()
        {
            this.service = new SuppliersService();
        }


        [Route("{type?}/")]
        public ActionResult All(string type)
        {
            IEnumerable<SupplierVm> viewModels = this.service.GetSuppliersFromDb(type);
            return View(viewModels);
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
            
            return View();
        }


        [HttpPost]
        [Route("add/")]
        public ActionResult Add([Bind] AddSupliersBm bind)
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
                this.service.AddSupplier(bind, loggedInUser.Id);
                return this.RedirectToAction("All");
            }
            AddSupplierVm vm = this.service.GetAddSupplierVm(bind);
            return View(vm);
            
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------
            DeleteSupplierVm vm = service.GetDeleteSupplierVm(id);
            return View(vm);
        }


        [HttpPost]
        [Route("delete/{id:int}")]
        public ActionResult Delete([Bind(Include = "Id")]DeleteSupplierBm bind)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------

            if (this.ModelState.IsValid)
            {
                User loggedInUser = AuthenticationManager.GetAuthenticatedUser(httpCookie.Value);
                this.service.DeleteSupplier(bind, loggedInUser.Id);
                return this.RedirectToAction("All");
            }

            DeleteSupplierVm vm = this.service.GetDeleteSupplierVm(bind.Id);
            return this.View(vm);
        }


        [HttpGet]
        [Route("edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------
           EditSupplierVm vm = service.GetEditSupplierVm(id);
           return View(vm);
        }


        [HttpPost]
        [Route("edit/{id:int}")]
        public ActionResult Edit([Bind]EditSupplierBm bind)
        {
            //--------check if user is NOT logged----------------------------------
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }
            //--------------------------------------------------------------------

            if (this.ModelState.IsValid)
            {
                User loggedInUser = AuthenticationManager.GetAuthenticatedUser(httpCookie.Value);
                this.service.EditSupplier(bind, loggedInUser.Id);
                return this.RedirectToAction("All");
            }

            EditSupplierVm vm = this.service.GetEditSupplierVm(bind.Id);
            return this.View(vm);
        }
    }
}
