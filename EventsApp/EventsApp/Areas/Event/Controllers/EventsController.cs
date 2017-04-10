using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventApp.Services;
using EventsApp.Attributies;
using EventsApp.Data;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using Microsoft.AspNet.Identity;

namespace EventsApp.Areas.Event.Controllers
{
    [RoutePrefix("Events")]
    public class EventsController : Controller
    {
        private EventService service;

        public EventsController()
        {
            this.service = new EventService();
        }

        // GET: Events
        [HttpGet]
        [AllowAnonymous]
        [Route("All/{categoryName?}")]
        public ActionResult All(string categoryName)
        {
            IEnumerable<EventAllVm> vm = this.service.GetEventAllVms(categoryName);
            return View("~/Areas/Event/Views/All.cshtml", vm);
        }

        [HttpPost]
        [MyAuthorize(Roles = "Visitor")]
        public ActionResult All([Bind] JoinInEventBm vm)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                this.service.AddEventToVisitor(currentUserId, vm.EventId);
                return RedirectToAction("All");
            }
            return Redirect(Request.UrlReferrer.ToString());

        }

        //// GET: Events/MyEvents
        [HttpGet]
        [Route("MyEvents")]
        public ActionResult MyEvents()
        {
            //string path = HttpContext.Request.Url.LocalPath;
            //TODO list events only for usre and selected by category

            string currentUserId = User.Identity.GetUserId();
            IEnumerable<EventAllVm> vms = this.service.GetMyEventsVms(currentUserId);
            return View("~/Areas/Event/Views/All.cshtml", vms);
        }

        // GET: Events/Create
        [HttpGet]
        [MyAuthorize(Roles = "Promoter")]
        [Authorize]
        [Route("Create")]
        public ActionResult Create()
        {
            return View("~/Areas/Event/Views/Create.cshtml");
        }

        //@Html.Action
        [ChildActionOnly]
        public ActionResult CategoriesListForCreateEvent()
        {
            IEnumerable<Category> categories = this.service.GetCategories(); 
            return PartialView("_CategoriesListForCreateEvent", categories);
        }

        //@Html.Action
        [ChildActionOnly]
        public ActionResult CategoriesListForEventAll()
        {
            IEnumerable<Category> categories = this.service.GetCategories();
            return PartialView("_CategoriesForEventAll", categories);
        }

        //@Html.Action
        [ChildActionOnly]
        public ActionResult LocationsListForEventAll()
        {
            IEnumerable<string> locations = this.service.GetLocations();
            return PartialView("_LocationsListForEventAll", locations);
        }

        //@Html.Action
        [ChildActionOnly]
        public ActionResult CommentListForDetailPage(int eventId)
        {
            IEnumerable<CommentVm> comments = this.service.GetCommentVms(eventId);
            return PartialView("~/Areas/Event/Views/Shared/_Comments.cshtml", comments);
        }



        // POST: Events/Create
        [HttpPost]
        [MyAuthorize(Roles = "Promoter")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] AddEventBm bind)
        {
            if (ModelState.IsValid)
            {
                //ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext()
                //    .GetUserManager<ApplicationUserManager>()
                //    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                //ApplicationUserManager manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

                string currentUserId = User.Identity.GetUserId();
                service.CreateEvent(bind, currentUserId);
                return RedirectToAction("All");
            }

            return View("~/Areas/Event/Views/Create.cshtml");
        }
       
        //// GET: Events/Details/5
        [Route("details/{id}")]
        public ActionResult Details(int id)
        {
            EventVm @event = this.service.GetEventVm(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View("~/Areas/Event/Views/Details.cshtml", @event);
        }


        //// GET: Events/{location}
        [HttpGet]
        [Route("Location/{name}")]
        public ActionResult Location(string name)
        {
            IEnumerable<EventAllVm> vm = this.service.GetEventByLocationVms(name);
            return View("~/Areas/Event/Views/All.cshtml", vm);
        }

        #region TEMP
        //// GET: Events/Edit/5
        //[MyAuthorize(Roles = "Promoter")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(@event);
        //}

        //// POST: Events/Edit/5
        // [MyAuthorize(Roles = "Promoter")]
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(@event).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(@event);
        //}

        //// GET: Events/Delete/5
        //[MyAuthorize(Roles = "Admin")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(@event);
        //}

        //// POST: Events/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[MyAuthorize(Roles = "Admin")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Event @event = db.Events.Find(id);
        //    db.Events.Remove(@event);
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
        #endregion


    }
}
