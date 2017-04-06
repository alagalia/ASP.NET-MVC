using System.Collections.Generic;
using System.Linq;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using EventsApp.Models.ViewModels.Visitor;

namespace EventApp.Services
{
    public class VisitorService :Service
    {
        public IEnumerable<MyEventVisitorVm> GetEveentsVms(string currentUserId)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(x => x.Id == currentUserId);
            IEnumerable<MyEventVisitorVm> vms = this.Context.Events.Select(e => new MyEventVisitorVm()
            {
                CategoryName = e.Category.Name,
                Id = e.Id,
                StartDateTime = e.StartDateTime,
                Title = e.Title
            });
            return vms;
        }
    }
}