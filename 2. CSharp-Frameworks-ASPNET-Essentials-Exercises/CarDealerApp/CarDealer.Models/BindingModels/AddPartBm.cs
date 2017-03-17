using System.Collections.Generic;
using CarDealer.Models.EntityModels;

namespace CarDealer.Models.BindingModels
{
    public class AddPartBm
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; }
    }
}