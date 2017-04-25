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
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartDateTime { get; set; }

        public string ImageUrl { get; set; }

        public int Rating { get; set; }

        public Category Category { get; set; }
    }
}