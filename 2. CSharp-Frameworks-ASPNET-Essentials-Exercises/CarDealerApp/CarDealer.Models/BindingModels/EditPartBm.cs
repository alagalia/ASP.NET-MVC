using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models.BindingModels
{
    public class EditPartBm
    {
        public int Id { get; set; }

        [Range(0, 100)]
        public double? Price { get; set; }
        public int Quantity { get; set; }
    }
}