using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using EventsApp.Models.EntityModels;
using EventsApp.Models.ViewModels.Event;
using EventsApp.Models.ViewModels.Visitor;

namespace EventApp.Services
{
    public class VisitorService : Service
    {
        public IEnumerable<MyEventVisitorVm> GetEveentsVms(string currentUserId)
        {
            Visitor visitor = this.Context.Visitors.FirstOrDefault(u => u.User.Id == currentUserId);
            if (visitor != null)
            {
                IEnumerable<Event> myEvents = visitor.Events;
                IEnumerable<MyEventVisitorVm> vms = Mapper.Map<IEnumerable<Event>, IEnumerable<MyEventVisitorVm>>(myEvents);
                return vms;
            }
            return null;
        }

        public void RemoveEventFormUserList(string currentUserId, int? id)
        {
            ApplicationUser currentUser = this.Context.Users.FirstOrDefault(x => x.Id == currentUserId);
            Visitor visitor = this.Context.Visitors.FirstOrDefault(user => user.User.Id == currentUser.Id);
            if (visitor != null)
            {
                Event ev = this.Context.Events.Find(id);
                visitor.Events.Remove(ev);
                Context.Entry(visitor).State = EntityState.Modified;
                this.Context.SaveChanges();
            }
        }
    }
}