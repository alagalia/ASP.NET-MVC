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
            //EditInfoPromoterVm vm = new EditInfoPromoterVm()
            //{
            //    Id = promoter.Id,
            //    Description = promoter.Description,
            //    Contacts = promoter.Contacts,
            //    Name = promoter.Name
            //};
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

        public IEnumerable<PromoterInfoVm> GetPromoterInfoVms()
        {
            IEnumerable<Promoter> promoters = this.Context.Promoters;
            return Mapper.Map<IEnumerable<Promoter>, IEnumerable<PromoterInfoVm>>(promoters);
        }
    }
}