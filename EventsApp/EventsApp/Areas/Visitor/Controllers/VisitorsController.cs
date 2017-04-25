using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using EventApp.Services;
using EventApp.Services.Intefaces;
using EventsApp.Models.ViewModels.Visitor;
using Microsoft.AspNet.Identity;

namespace EventsApp.Areas.Visitor.Controllers
{
    public class VisitorsController : Controller
    {
        private IVisitorService service;

        public VisitorsController(IVisitorService service)
        {
            this.service = service;
        }
        // GET: Visitor/MyEvents
        public ActionResult MyEvents()
        {
            string currentUserId = User.Identity.GetUserId();
            IEnumerable<MyEventVisitorVm> vms = this.service.GetEveentsVms(currentUserId);
            return View("~/Areas/Visitor/Views/MyEvents.cshtml", vms);
        }

        // GET: Visitor/Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                this.service.RemoveEventFormUserList(currentUserId, id);
            }
            return RedirectToAction("MyEvents");
        }
    }
}
