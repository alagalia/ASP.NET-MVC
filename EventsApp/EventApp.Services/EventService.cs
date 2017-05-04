using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using EventApp.Services.Intefaces;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;

namespace EventApp.Services
{
    public class EventService : Service, IEventService
    {
        public IEnumerable<Category> GetCategories()
        {
            return this.Context.Categories;
        }

        public void CreateEvent(AddEventBm bind, string userId)
        {
            Promoter promoter = this.Context.Promoters.FirstOrDefault(p => p.User.Id == userId);
            if (promoter == null)
            {
                promoter = new Promoter()
                {
                    User = this.Context.Users.Find(userId)
                };
            };

            Category catg = this.Context.Categories.Find(bind.CategoryId);
            Event ev = Mapper.Map<AddEventBm, Event>(bind);
            ev.Category = catg;
            ev.Owner = promoter;
            this.Context.Events.Add(ev);
            this.Context.SaveChanges();
        }

        public EventDetailsVm GetEventDetailsVm(int id)
        {
            Event ev = this.Context.Events.Find(id);
            if (ev == null)
            {
                return null;
            }
            
            EventDetailsVm eventVm = Mapper.Map<Event, EventDetailsVm>(ev);
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
            IEnumerable<EventAllVm> vms = Mapper.Map<IEnumerable<Event>, IEnumerable<EventAllVm>>(this.Context.Events);
            return vms;
        }

        public IEnumerable<EventAllVm> GetEventAllVms(string categoryName)
        {
            if (categoryName == null)
            {
                return Mapper.Map<IEnumerable<Event>, IEnumerable<EventAllVm>>(this.Context.Events);
            }
            Category category = this.Context.Categories.FirstOrDefault(c => c.Name == categoryName);
            IEnumerable<Event> events = this.Context.Events.Where(e=>e.Category.Name == categoryName);

            return category != null ? Mapper.Map<IEnumerable<Event>, IEnumerable<EventAllVm>>(events) : null;
        }

        public IEnumerable<EventAllVm> GetMyEventsVms(string currentUserId)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(x => x.Id == currentUserId);
            IEnumerable<EventAllVm> vms = Mapper.Map<IEnumerable<Event>, IEnumerable<EventAllVm>>(this.Context.Events.Where(e => e.Owner.User.Id == currentUser.Id));
           return vms;
        }

        public IEnumerable<string> GetLocations()
        {
            IEnumerable<string> locations = this.Context.Events.ToList().Select(e => e.Location);
            locations = new HashSet<string>(locations);
            return locations;
        }

        public IEnumerable<EventAllVm> GetEventByLocationVms(string location)
        {
            IEnumerable<Event> events = this.Context.Events.Where(e => e.Location == location);
            IEnumerable<EventAllVm> vms= Mapper.Map<IEnumerable<Event>, IEnumerable<EventAllVm>>(events);
            return vms;
        }


        public IEnumerable<CommentVm> GetCommentVms(int eventId)
        {
            IEnumerable<Comment> comments = this.Context.Events.Find(eventId).Comments;
            IEnumerable<CommentVm> vms = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentVm>>(comments);
            return vms;
        }

        public void DeleteEvent(int id)
        {
            Event ev = this.Context.Events.Find(id);
            this.Context.Events.Remove(ev);
            this.Context.SaveChanges();
        }

        public void AddComment(string currentUserId, CommentBm bm)
        {
            Event ev = this.Context.Events.Find(bm.EventId);
            Comment  comment  = Mapper.Map<CommentBm,Comment>(bm);
            comment.User = this.Context.Users.FirstOrDefault(x => x.Id == currentUserId);
            ev.Comments.Add(comment);
            Context.SaveChanges();

        }

        public int CalculateRating()
        {
            int result = this.Context.Events.Sum(e => e.Rating)/ this.Context.Events.Count() % 5;
            return result;
        }

        public void VoteUp(int id)
        {
            Event ev = this.Context.Events.Find(id);
            ev.Rating++;
            Context.Entry(ev).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public void VoteDown(int id)
        {
            Event ev = this.Context.Events.Find(id);
            ev.Rating--;
            Context.Entry(ev).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
    }
}
