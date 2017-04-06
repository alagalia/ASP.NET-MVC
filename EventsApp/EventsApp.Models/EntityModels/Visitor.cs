using System.Collections.Generic;

namespace EventsApp.Models.EntityModels
{
    public class Visitor
    {
        public Visitor()
        {
            this.Events = new HashSet<Event>();
        }

        public int Id { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}