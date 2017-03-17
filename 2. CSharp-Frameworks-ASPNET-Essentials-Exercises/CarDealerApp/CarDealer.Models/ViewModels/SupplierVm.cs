namespace CarDealer.Models.ViewModels
{
    public class SupplierVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsImporter { get; set; }
        public int NumberOfPartsToSupply { get; set; }
    }
}