using System;
using System.ComponentModel.DataAnnotations;
using EventsApp.Models.EntityModels;

namespace EventsApp.Models.ViewModels.Event
{
    public class EventBriefVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name ="Start Date")]
        public DateTime StartDateTime { get; set; }

        public string ImageUrl { get; set; }

        public Category Category { get; set; }
    }
}