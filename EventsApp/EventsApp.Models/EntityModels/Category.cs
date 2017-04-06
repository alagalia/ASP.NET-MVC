using System.Collections.Generic;

namespace EventsApp.Models.EntityModels
{
    public class Category
    {
        public Category()
        {
            this.Events = new HashSet<Event>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Event> Events { get; set; }
    }
}