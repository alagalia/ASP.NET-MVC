using System;
using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.ViewModels.Visitor
{
    public class MyEventVisitorVm
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDateTime { get; set; }

        public string CategoryName { get; set; }
    }
}