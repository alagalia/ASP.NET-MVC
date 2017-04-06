using System.Collections.Generic;

namespace EventsApp.Models.EntityModels
{
    public class Promoter
    {
        public Promoter()
        {
            this.Events = new HashSet<Event>();
        }
        public int Id { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual PromoterInfo Info { get; set; }

        public virtual ApplicationUser  User { get; set; }
    }
}