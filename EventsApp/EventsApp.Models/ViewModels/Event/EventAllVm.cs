using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.ViewModels.Event
{
    public class EventAllVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDateTime { get; set; }

        public string Location { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        [StringLength(11)]
        public string YouTubeUrl { get; set; }

        public int OwnerId { get; set; }

    }
}