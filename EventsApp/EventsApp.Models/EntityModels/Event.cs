using System.ComponentModel.DataAnnotations.Schema;

namespace EventsApp.Models.EntityModels
{
    public class Event
    {
        public int Id { get; set; }

        public string  Title { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Category  Category { get; set; }

    }
}