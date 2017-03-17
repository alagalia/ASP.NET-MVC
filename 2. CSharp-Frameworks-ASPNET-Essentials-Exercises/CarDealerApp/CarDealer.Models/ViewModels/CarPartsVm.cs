using System.Collections.Generic;

namespace CarDealer.Models.ViewModels
{
    public class CarPartsVm
    {
        public CarVm Car { get; set; }

        public IEnumerable<PartVm> Parts { get; set; }
    }
}