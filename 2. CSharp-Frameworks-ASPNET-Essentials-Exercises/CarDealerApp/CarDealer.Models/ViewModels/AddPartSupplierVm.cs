using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models.ViewModels
{
    public class AddPartSupplierVm
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}