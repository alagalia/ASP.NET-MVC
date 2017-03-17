using System.Collections.Generic;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealer.Services;

namespace CarDealerApp.Controllers
{
    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        private CustomersService service;

        public CustomersController()
        {
            this.service = new CustomersService();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            EditCustomerVm model = this.service.GetEditCustomerVm(id);
            return this.View(model);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Name, BirthDate")] EditCustomerBm bind)
        {
            if (ModelState.IsValid)
            {
                this.service.EditCustomer(bind);
                return this.RedirectToAction("All", new { order = "ascending" });

            }
            EditCustomerVm vm = this.service.GetEditCustomerVm(bind.Id);
            return this.View(vm);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Name, BirthDate")] AddCustomerBm bind) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(bind);
            }
            if (bind != null)
            {
                this.service.AddCustomerBm(bind);
                return this.RedirectToAction("All", new { order = "ascending" });
            }
            return View();
        }

        [HttpGet]
        [Route("all/{order}")]
        public ActionResult All(string order)
        {
            IEnumerable<AllCustomerVm> viewModels = this.service.GetAllOrderedCustomers(order);
            return this.View(viewModels);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult About(int id)
        {
            AboutCustomerVm viewModels = this.service.GetAboutCustomers(id);
            return this.View(viewModels);
        }

    }
}