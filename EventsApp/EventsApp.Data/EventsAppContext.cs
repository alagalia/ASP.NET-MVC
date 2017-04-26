using System.Data.Entity;
using EventsApp.Models.EntityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventsApp.Data
{
    public class EventsAppContext : IdentityDbContext<ApplicationUser>
    {
        public EventsAppContext()
            : base("EventsApp", throwIfV1Schema: false)
        {

        }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Promoter> Promoters { get; set; }

        public virtual DbSet<Visitor> Visitors { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public static EventsAppContext Create()
        {
            return new EventsAppContext();
        }
    }
}