using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventApp.Services;
using EventsApp.Data;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using EventsApp.Models.ViewModels.Promoter;
using Microsoft.AspNet.Identity;

namespace EventsApp.Controllers
{
    [RoutePrefix("Promoters")]
    public class PromotersController : Controller
    {
        private EventsAppContext db = new EventsAppContext();

        private PromoterService service;

        public PromotersController()
        {
            this.service = new PromoterService();
        }

        // GET: Promoters
        [HttpGet]
        [Route("All")]
        public ActionResult All()
        {
            //todo make method in service remove db
            var promoters = db.Promoters.Include(p => p.Info);
            return View(promoters.ToList());
        }

        //todo make action profile of promoter with all his events
        

        // GET: Promoters/Details/5
        [HttpGet]
        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            PromoterAllInfoVm promoter = this.service.GetPromoterAllInfoVm(id);
            if (promoter == null)
            {
                return HttpNotFound();
            }
            return View(promoter);
        }

        // GET: Promoters/EditProfile
        [HttpGet]
        [Route("EditProfile")]
        public ActionResult EditProfile()
        {
            string currentUserId = User.Identity.GetUserId();
            EditInfoPromoterVm promoter = this.service.GetEditUserPtofileVm(currentUserId );
            if (promoter == null)
            {
                return HttpNotFound();
            }
            return View(promoter);
        }

        // GET: Promoters/EditProfile
        [HttpPost]
        public ActionResult EditProfile([Bind] EditInfoPromoterBm bm)
        {
            this.service.EditInfoPromoter(bm);
            return RedirectToAction("Details", new { id = bm.Id });
        }


        //@Html.Action
        [ChildActionOnly]
        public ActionResult EventList(IEnumerable<EventBriefVm> eventBriefVms)
        {
            return PartialView("_DisplayEventBriefVmForPromoterProfile", eventBriefVms);
        }

        //// GET: Promoters/Edit/5
        //public ActionResult Edit()
        //{
        //    int id = 555555555;
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Promoter promoter = db.Promoters.Find(id);
        //    if (promoter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Id = new SelectList(db.UserInfos, "Id", "Name", promoter.Id);
        //    return View(promoter);
        //}

        //// POST: Promoters/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id")] Promoter promoter)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(promoter).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Id = new SelectList(db.UserInfos, "Id", "Name", promoter.Id);
        //    return View(promoter);
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
