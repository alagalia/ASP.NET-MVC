using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.BindingModels
{
    public class AddEventBm
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDateTime { get; set; }

        public string Location { get; set; }

        public string Image { get; set; }

        [StringLength(11)]
        public string YouTubeUrl { get; set; }

        public int CategoryId { get; set; }
    }
}