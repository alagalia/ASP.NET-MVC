using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventApp.Services.Intefaces;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;

namespace EventApp.Services
{
    public class HomeService :Service, IHomeService
    {
        public IEnumerable<EventBriefVm> Get3RecentlyEventsBriefVms()
        {
            IEnumerable<Event> eventsFirst3 =  this.Context.Events.OrderBy(e => e.StartDateTime).Take(3);
            IEnumerable<EventBriefVm> vms = Mapper.Map<IEnumerable<Event>, IEnumerable<EventBriefVm>>(eventsFirst3);
            return vms;
        }
    }
}