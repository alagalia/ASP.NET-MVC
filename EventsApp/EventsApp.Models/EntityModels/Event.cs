using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.EntityModels
{
    public class Event
    {
        public int Id { get; set; }

        public string  Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public byte[] Image { get; set; }

        //[StringLength(11)]
        public string YouTubeUrl { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual Category  Category { get; set; }

    }
}