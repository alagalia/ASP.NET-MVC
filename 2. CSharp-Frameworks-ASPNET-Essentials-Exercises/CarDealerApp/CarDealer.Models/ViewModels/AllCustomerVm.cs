using System;

namespace CarDealer.Models.ViewModels
{
    public class AllCustomerVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsYoungDriver { get; set; }
    }
}