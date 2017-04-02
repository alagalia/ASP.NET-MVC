using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;

namespace EventApp.Services
{
    public class EventService : Service
    {
        public IEnumerable<Category> GetCategories()
        {
            return this.Context.Categories;
        }

        public void CreateEvent(AddEventBm bind, ApplicationUser user)
        {
            Promoter promoter = this.Context.Promoters.FirstOrDefault(p => p.User.Id == user.Id) ?? new Promoter()
            {
                User = user
            };

            Category catg = this.Context.Categories.Find(bind.CategoryId);
            Event ev = new Event()
            {
                Title = bind.Title,
                Description = bind.Description,
                StartDateTime = DateTime.Now,
                Owner = promoter,
                Category = catg,
                ImageUrl = bind.Image,
                YouTubeUrl = bind.YouTubeUrl
            };

            this.Context.Events.Add(ev);
            this.Context.SaveChanges();
        }

        public EventVm GetEventVm(int id)
        {
            Event ev = this.Context.Events.Find(id);
            if (ev == null)
            {
                return null;
            }

            EventVm eventVm = Mapper.Map<Event, EventVm>(ev);
            return eventVm;
        }

        public void AddEventToVisitor(ApplicationUser currentUser, int? eventId)
        {
            Visitor currentVisitor = this.Context.Visitors.FirstOrDefault(visitor => visitor.User.Id == currentUser.Id);
            Event ev = this.Context.Events.Find(eventId); 
            currentVisitor.Events.Add(ev);
            Context.SaveChanges();
        }

        public IEnumerable<EventAllVm> GetEventAllVms()
        {
            IEnumerable<EventAllVm> vms = this.Context.Events.Select(@event => new EventAllVm()
            {
                CategoryName = @event.Category.Name,
                Description = @event.Description,
                Id = @event.Id,
                ImageUrl = @event.ImageUrl,
                OwnerId = @event.Owner.Id,
                StartDateTime = @event.StartDateTime,
                Title = @event.Title,
                YouTubeUrl = @event.YouTubeUrl
            });
            return vms;
        }
    }
}
