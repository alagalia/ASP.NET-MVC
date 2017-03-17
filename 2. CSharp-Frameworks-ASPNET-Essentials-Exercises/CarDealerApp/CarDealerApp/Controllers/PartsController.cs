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
    [RoutePrefix("parts")]
    public class PartsController : Controller
    {
        private PartsService service;

        public PartsController()
        {
            this.service = new PartsService();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            EditPartVm vm = this.service.GetEditVm(id);
            return this.View(vm);
        }

        //TODO
        //[HttpPost]
        //[Route("edit/{id}")]
        //public ActionResult Edit([Bind(Include = "Id, Price, Quantity")] EditPartBm bind)
        //{
        //    EditPartVm vm = this.service.GetEditVm(bind);
        //    return this.View(vm);
        //}

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            DeletePartVm vm = this.service.GetDeleteVm(id);
            return this.View(vm);
        }

        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete([Bind(Include = "PartId")] DeletePartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.DeletePart(bind);
                return this.RedirectToAction("All", "Parts");
            }
            return this.View(this.service.GetDeleteVm(bind.PartId));
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            IEnumerable<AddPartSupplierVm> suppliers = this.service.GetAddPartSuppliersVm();
            return this.View(suppliers);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Name, Price, Quantity, SupplierId")] AddPartBm bind)
        {
            if (this.ModelState.IsValid)
            {
                this.service.AddPart(bind);
                return this.RedirectToAction("All", "Parts");
            }
            return this.View(this.service.GetAddVm());
        }

        [Route("all")]
        public ActionResult All()
        {
            IEnumerable<AllPartVm> vms = this.service.GetAllPartVms();
            return View(vms);
        }



        //// GET: Parts/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Part part = db.Parts.Find(id);
        //    if (part == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(part);
        //}

        //// GET: Parts/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Parts/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Price,Quantity")] Part part)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Parts.Add(part);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(part);
        //}

        //// GET: Parts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Part part = db.Parts.Find(id);
        //    if (part == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(part);
        //}

        //// POST: Parts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Price,Quantity")] Part part)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(part).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(part);
        //}

        //// GET: Parts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Part part = db.Parts.Find(id);
        //    if (part == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(part);
        //}

        //// POST: Parts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Part part = db.Parts.Find(id);
        //    db.Parts.Remove(part);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
