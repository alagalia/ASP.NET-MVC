using System.Collections.Generic;
using System.Linq;
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

            //IEnumerable<EventAllVm> vms =
            //    this.Context.Events.Where(e => e.Owner.User.Id == currentUser.Id).Select(@event => new EventAllVm()
            //    {
            //        CategoryName = @event.Category.Name,
            //        Description = @event.Description,
            //        Id = @event.Id,
            //        ImageUrl = @event.ImageUrl,
            //        StartDateTime = @event.StartDateTime,
            //        Title = @event.Title,
            //        Location = @event.Location,
            //        YouTubeUrl = @event.YouTubeUrl
            //    });
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

        public void AddComment(int id, CommentBm comments)
        {
            Event ev = this.Context.Events.Find(id);
            Comment  comment  = Mapper.Map<CommentBm,Comment>(comments);
            ev.Comments.Add(comment);
            Context.SaveChanges();

        }
    }
}
