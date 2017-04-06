using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.ViewModels.Event
{
    public class EventVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public string ImageUrl { get; set; }

        [StringLength(11)]
        public string YouTubeUrl { get; set; }

        public string UserId { get; set; }

        public string Category { get; set; }
    }
}