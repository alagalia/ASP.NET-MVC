using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.BindingModels
{
    public class AddEventBm
    {
        [Required(ErrorMessage = "Title required!"), MinLength(5, ErrorMessage = "Title cannot be less than 5 chars!"), MaxLength(30, ErrorMessage= "Title cannot be more than 30 chars!")]
        public string Title { get; set; }

        [Required, MinLength(5, ErrorMessage = "Description cannot be less than 5 chars!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required in yyyy-MM-dd format")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "Location required!")]
        public string Location { get; set; }
        
        public string Address { get; set; }

        [Required(ErrorMessage = "Image URL required!")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "YouTubeUrl have to be exact 11 chars!"), MinLength(11, ErrorMessage = "YouTubeUrl have to be exact 11 chars!"), MaxLength(11, ErrorMessage = "YouTubeUrl have to be exact 11 chars!")]
        [StringLength(11)]
        public string YouTubeUrl { get; set; }

        //[CategoryId]
        public int CategoryId { get; set; }
    }
}