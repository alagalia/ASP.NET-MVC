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

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string  Title { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [StringLength(11)]
        public string YouTubeUrl { get; set; }

        public virtual Promoter Owner { get; set; }

        public virtual Category  Category { get; set; }

        public virtual ICollection<Visitor> Visitors { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

    }
}