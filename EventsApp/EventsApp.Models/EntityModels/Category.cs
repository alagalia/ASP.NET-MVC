using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.EntityModels
{
    public class Category
    {
        public Category()
        {
            this.Events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Event> Events { get; set; }
    }
}