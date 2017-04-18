using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EventsApp.Models.EntityModels;

namespace EventsApp.Models.ViewModels.Event
{
    public class EventVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime StartDateTime { get; set; }

        public string ImageUrl { get; set; }

        public string Location { get; set; }

        public string YouTubeUrl { get; set; }

        public string UserId { get; set; }

        public string Category { get; set; }

        public int CommentsCounter { get; set; }

    }
}