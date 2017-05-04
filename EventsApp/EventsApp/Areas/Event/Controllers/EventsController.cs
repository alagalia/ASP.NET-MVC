using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using EventApp.Services;
using EventApp.Services.Intefaces;
using EventsApp.Attributies;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using Microsoft.AspNet.Identity;
using PagedList;

namespace EventsApp.Areas.Event.Controllers
{
    [RoutePrefix("Events")]
    public class EventsController : Controller
    {
        private IEventService service;

        public EventsController(IEventService service)
        {
            this.service = service;
        }

        // GET: Events
        [HttpGet]
        [AllowAnonymous]
        [Route("All/{categoryName?}")]
        public ActionResult All(string categoryName, int? pageNumber)
        {
            IEnumerable<EventAllVm> vm = this.service.GetEventAllVms(categoryName).ToList().ToPagedList(pageNumber ?? 1, 6); 
            if (vm == null)
            {
                return HttpNotFound();
            }
            this.ViewBag.Rating = this.service.CalculateRating();
            return View("~/Areas/Event/Views/All.cshtml", vm);
        }

        //// GET: Events/{location}
        [HttpGet]
        [Route("Location/{name}")]
        public ActionResult Location(string name, int? pageNumber)
        {
            if (name == null)
            {
                name = (string)this.RouteData.Values["id"];
            }

            IEnumerable<EventAllVm> vm = this.service.GetEventByLocationVms(name).ToList().ToPagedList(pageNumber ?? 1, 6); ;
            return View("~/Areas/Event/Views/All.cshtml", vm);
        }


        //GET: Events/MyEvents
        [HttpGet]
        [Route("MyEvents")]
        [MyAuthorize(Roles = "Promoter")]
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
            return View("~/Areas/Event/Views/Create.cshtml", bind);
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



        // POST: Events/Delete/5 
        [HttpPost]
        [Route("Delete")]
        //[ValidateAntiForgeryToken]
        [MyAuthorize(Roles = "Admin, Promoter")]
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
        }


        // POST: Events/Comment?Id
        [HttpPost]
        [Route("Comment/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Comment([Bind] CommentBm bind)
        {
            if (bind.EventId <=0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string currentUserId = User.Identity.GetUserId();

            this.service.AddComment(currentUserId, bind);
            return Redirect(this.Request.UrlReferrer.AbsolutePath);
        }
        
        
        //join in to event
        [HttpPost]
        [MyAuthorize(Roles = "Visitor")] 
        public ActionResult All([Bind] JoinInEventBm vm)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                this.service.AddEventToVisitor(currentUserId, vm.EventId);
                return RedirectToAction("MyEvents", "Visitors");
            }
            return Redirect(Request.UrlReferrer.ToString());

        }
        // POST: Events/VoteUp/{id}

        [HttpPost]
        [Route("VoteUp/{id}")]
        [MyAuthorize(Roles = "Visitor, Admin")]
        public ActionResult VoteUp(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            service.VoteUp(id);
            return RedirectToAction("All");
        }

        [HttpPost]
        [Route("VoteDown/{id}")]
        [MyAuthorize(Roles = "Visitor, Admin")]
        public ActionResult VoteDown(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            service.VoteDown(id);
            return Redirect(this.Request.UrlReferrer.AbsolutePath);
        }

        //---------------------ACTIONS -------------------


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
        //[ChildActionOnly]
        public ActionResult CommentListForDetailPage()
        {
            string id = (string)this.RouteData.Values["Id"];
            IEnumerable<CommentVm> comments = this.service.GetCommentVms(int.Parse(id));
            return PartialView("~/Areas/Event/Views/Shared/_Comments.cshtml", comments);
        }
    }
}
