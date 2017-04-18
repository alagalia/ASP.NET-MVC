using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.BindingModels
{
    public class AddEventBm
    {
        [MinLength(5), MaxLength(30, ErrorMessage= "Title cannot be more than 30 characters!")]
        public string Title { get; set; }

        [MaxLength(300, ErrorMessage = "Title cannot be more than 30 characters!")]
        public string Description { get; set; }

        [Display(Name = "Start Date & Time")]
        public DateTime StartDateTime { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Image { get; set; }

        [StringLength(11)]
        public string YouTubeUrl { get; set; }

        [Range(1, 100)]
        public int CategoryId { get; set; }
    }
}