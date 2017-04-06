using System.Collections.Generic;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;

namespace EventsApp.Models.ViewModels.Promoter
{
    public class PromoterAllInfoVm
    {
        //public int Id { get; set; }

        public IEnumerable<EventBriefVm> Events { get; set; }

        public PromoterInfoVm Info { get; set; }
    }
}