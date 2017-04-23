using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using EventApp.Services;
using EventsApp.Attributies;
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
            if (vm == null)
            {
                return HttpNotFound();
            }
            return View("~/Areas/Event/Views/All.cshtml", vm);
        }

        [HttpPost]
        [MyAuthorize(Roles = "Visitor")] //TODO
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
            string currentUserId = User.Identity.GetUserId();
            IEnumerable<EventAllVm> vms = this.service.GetMyEventsVms(currentUserId);
            return View("~/Areas/Event/Views/AllMy.cshtml", vms);
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
            return PartialView("~/Areas/Event/Views/Shared/_CategoriesListForCreateEvent.cshtml", categories);
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
            ViewBag.Eventid = id;
            EventDetailsVm @event = this.service.GetEventDetailsVm(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View("~/Areas/Event/Views/Details.cshtml", @event);
        }

        // POST: Events/Delete/5 //TODO: fix action name with delete
        [HttpPost]
        [Route("Delete")]
        //[ValidateAntiForgeryToken]
        //[MyAuthorize(Roles = "Admin|Promoter")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ev = this.service.GetEventDetailsVm(id);
            if (ev == null)
            {
                return HttpNotFound();
            }
            this.service.DeleteEvent(id);
            return RedirectToAction("All");

            //return RedirectToAction("Details", new { id });

        }

        // POST: Events/Comment?Id= 
        [HttpPost]
        [Route("Comment/{id}")]
        //[ValidateAntiForgeryToken]
        public ActionResult Comment([Bind] CommentBm bind)
        {
            var param = RouteData.Values["Id"];
            if (param == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = int.Parse(param.ToString());
            if (id <=0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            this.service.AddComment(id, bind);
            return Redirect(this.Request.UrlReferrer.AbsolutePath);

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
