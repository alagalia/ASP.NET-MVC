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

        public void CreateEvent(AddEventBm bind, string userId)
        {
            Promoter promoter = this.Context.Promoters.FirstOrDefault(p => p.User.Id == userId);
            if(promoter == null)
            {
                promoter = new Promoter()
                {
                    User = this.Context.Users.Find(userId)
                };
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

        public void AddEventToVisitor(string currentUserId, int? eventId)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(x => x.Id == currentUserId);

            Visitor currentVisitor = this.Context.Visitors.FirstOrDefault(visitor => visitor.User.Id == currentUser.Id);
            Event ev = this.Context.Events.Find(eventId);
            currentVisitor.Events.Add(ev);
            Context.SaveChanges();
        }

        public IEnumerable<EventAllVm> GetEventAllVms()
        {
            IEnumerable<EventAllVm> vms =
                this.Context.Events.Select(@event => new EventAllVm()
                {
                    CategoryName = @event.Category.Name,
                    Description = @event.Description,
                    Location = @event.Location,
                    Id = @event.Id,
                    ImageUrl = @event.ImageUrl,
                    OwnerId = @event.Owner.Id,
                    StartDateTime = @event.StartDateTime,
                    Title = @event.Title,
                    YouTubeUrl = @event.YouTubeUrl
                });
            return vms;
        }

        public IEnumerable<EventAllVm> GetEventAllVms(string currentUserId)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(x => x.Id == currentUserId);

            IEnumerable<EventAllVm> vms =
                this.Context.Events.Where(e => e.Owner.User.Id == currentUser.Id).Select(@event => new EventAllVm()
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
