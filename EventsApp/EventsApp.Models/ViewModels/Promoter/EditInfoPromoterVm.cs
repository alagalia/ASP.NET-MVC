using EventsApp.Models.EntityModels;

namespace EventsApp.Models.ViewModels.Promoter
{
    public class EditInfoPromoterVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Contacts { get; set; }
    }
}