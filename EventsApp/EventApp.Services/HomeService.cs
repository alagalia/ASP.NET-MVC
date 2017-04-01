using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;

namespace EventApp.Services
{
    public class HomeService :Service
    {
        //get first 6 recently events
        public IEnumerable<EventBriefVm> Get6RecentlyEventsBriefVms()
        {
            IEnumerable<Event> eventsFirst6 =  this.Context.Events.OrderBy(e => e.StartDateTime).Take(3);
            IEnumerable<EventBriefVm> vms = eventsFirst6.Select(e => new EventBriefVm()
            {
                Category = e.Category,
                Id = e.Id,
                ImageUrl = e.ImageUrl,
                StartDateTime = e.StartDateTime,
                Title = e.Title
            });
            return vms;
        }
    }
}