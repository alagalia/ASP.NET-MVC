using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using EventApp.Services;
using EventsApp.Models.ViewModels.Visitor;
using Microsoft.AspNet.Identity;

namespace EventsApp.Areas.Visitor.Controllers
{
    public class VisitorsController : Controller
    {
        private VisitorService service;

        public VisitorsController()
        {
            this.service = new VisitorService();
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
            return RedirectToAction("MyEvents");
        }
    }
}
