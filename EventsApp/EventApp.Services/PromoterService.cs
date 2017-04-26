using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using EventApp.Services.Intefaces;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using EventsApp.Models.ViewModels.Promoter;

namespace EventApp.Services
{
    public class PromoterService : Service, IPromoterService
    {
        public PromoterDetailsInfoVm GetPromoterAllInfoVm(int id)
        {
            IEnumerable<Event> currentEvents = this.Context.Events.ToList().Where(e => e.Owner.Id == id);
            IEnumerable<EventBriefVm> cuurentEventBriefVms = Mapper.Map<IEnumerable<Event>, IEnumerable<EventBriefVm>>(currentEvents);
            PromoterInfoVm promoterInfoVm = null;
            Promoter promoter = this.Context.Promoters.Find(id);
            if (promoter != null)
            {
                promoterInfoVm =
                    Mapper.Map<Promoter, PromoterInfoVm>(promoter);
            }
            PromoterDetailsInfoVm vm = new PromoterDetailsInfoVm()
            {
                Events = cuurentEventBriefVms,
                Info = promoterInfoVm
            };
            return vm;
        }

        public EditInfoPromoterVm GetEditUserPtofileVm(string currentUserId)
        {
            Promoter promoter = this.Context.Promoters.FirstOrDefault(p => p.User.Id == currentUserId);
            EditInfoPromoterVm vm = Mapper.Map<Promoter, EditInfoPromoterVm>(promoter);
            return vm;
        }

        public void EditInfoPromoter(EditInfoPromoterBm bm)
        {
            Promoter promoter = this.Context.Promoters.Find(bm.Id);
            promoter.Contacts = bm.Contacts;
            promoter.Name = bm.Name;
            promoter.Description = bm.Description;

            Context.Entry(promoter).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void EditEventInfo(EditEventBm bm)
        {
            Event ev = this.Context.Events.Find(bm.Id);
            if (ev == null)
            {
                return;
            }
            ev.Title = bm.Title;
            ev.Location = bm.Location;
            ev.StartDateTime = bm.StartDateTime;
            ev.Address = bm.Address;
            ev.Description = bm.Description;
            ev.ImageUrl = bm.ImageUrl;
            ev.YouTubeUrl = bm.YouTubeUrl;
            ev.Category = this.Context.Categories.Find(bm.CategoryId);

            Context.Entry(ev).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public IEnumerable<PromoterInfoVm> GetPromoterInfoVms()
        {
            IEnumerable<Promoter> promoters = this.Context.Promoters;
            return Mapper.Map<IEnumerable<Promoter>, IEnumerable<PromoterInfoVm>>(promoters);
        }

        public EventDetailsVm GetEventInfoVm(int id)
        {
            Event ev = this.Context.Events.Find(id);
            return Mapper.Map<Event, EventDetailsVm>(ev);
        }
        
    }
}