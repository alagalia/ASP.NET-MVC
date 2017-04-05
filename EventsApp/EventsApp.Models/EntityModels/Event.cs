using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.EntityModels
{
    public class Event
    {
        public Event()
        {
            this.Visitors = new HashSet<Visitor>();
        }
        public int Id { get; set; }

        public string  Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        [StringLength(11)]
        public string YouTubeUrl { get; set; }

        public virtual Promoter Owner { get; set; }

        public virtual Category  Category { get; set; }

        public virtual ICollection<Visitor> Visitors { get; set; }

    }
}