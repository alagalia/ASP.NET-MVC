using System.ComponentModel.DataAnnotations;

namespace EventsApp.Models.EntityModels
{
    public class PromoterInfo
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Contacts { get; set; }

        [Required]
        public virtual Promoter Promoter { get; set; }
    }
}