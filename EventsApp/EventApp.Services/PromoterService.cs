using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using EventsApp.Models.ViewModels.Promoter;

namespace EventApp.Services
{
    public class PromoterService : Service
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

        public EditInfoPromoterVm GetEditUserPtofileVm(string currentUserId)
        {
            Promoter promoter = this.Context.Promoters.FirstOrDefault(p => p.User.Id == currentUserId);
            EditInfoPromoterVm vm = new EditInfoPromoterVm()
            {
                Id = promoter.Id,
                Description = promoter.Info.Description,
                Contacts = promoter.Info.Contacts,
                Name = promoter.Info.Name
            };
            return vm;
        }

        public void EditInfoPromoter(EditInfoPromoterBm bm)
        {
            PromoterInfo promoter = new PromoterInfo()
            {
                Id = bm.Id,
                Contacts = bm.Contacts,
                Name = bm.Name,
                Description = bm.Description,
                Promoter = this.Context.Promoters.Find(bm.Id)
        };
            Context.Entry(promoter).State = EntityState.Modified;
            this.Context.SaveChanges();

            //if (promoter == null) return;
            //promoter.Info.Contacts = bm.Contacts;
            //promoter.Info.Name = bm.Name;
            //promoter.Info.Description = bm.Description;
            //this.Context.SaveChanges();
        }
    }
}