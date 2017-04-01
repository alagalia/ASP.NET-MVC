using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using EventsApp.Models.ViewModels.Promoter;

namespace EventApp.Services
{
    public class PromoterService :Service
    {
        public PromoterAllInfoVm GetPromoterAllInfoVm(int id)
        {
            IEnumerable<Event> currentEvents = this.Context.Events.ToList().Where(e => e.Owner.Id == id);
            //IEnumerable<EventBriefVm> cuurentEventBriefVms = Mapper.Map<IEnumerable<Event>, IEnumerable<EventBriefVm>>(currentEvents);
            IEnumerable<EventBriefVm> cuurentEventBriefVms = currentEvents.Select(e => new EventBriefVm()
            {
                Category = e.Category,
                Id = e.Id,
                ImageUrl = e.ImageUrl,
                StartDateTime = e.StartDateTime,
                Title = e.Title
            });
            PromoterInfoVm promoterInfoVm = null;
            Promoter promoter = this.Context.Promoters.Find(id);
            if (promoter != null)
            {
                promoterInfoVm =
                    Mapper.Map<PromoterInfo, PromoterInfoVm>(promoter.Info);
            }
            PromoterAllInfoVm vm = new PromoterAllInfoVm()
            {
                Events = cuurentEventBriefVms,
                Info = promoterInfoVm
            };
            return vm;
        }
    }
}