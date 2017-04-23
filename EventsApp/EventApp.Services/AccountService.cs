using System.Linq;
using EventsApp.Models.BindingModels;
using EventsApp.Models.EntityModels;
using Microsoft.AspNet.Identity;

namespace EventApp.Services
{
    public class AccountService :Service
    {
        public void AddInfoToPromoter(AddInfoAccountBm bind, string currentuserId)
        {
            Promoter promoter = this.Context.Promoters.FirstOrDefault(p => p.User.Id == currentuserId);
            promoter.Contacts = bind.Contacts;
            promoter.Description = bind.Description;
            promoter.Name = bind.Name;
            this.Context.SaveChanges();
        }

        public void AddPromoter(ApplicationUser user)
        {
            Promoter promoter = new Promoter();
            ApplicationUser currentUser = this.Context.Users.Find(user.Id);
            promoter.User = currentUser;
            this.Context.Promoters.Add(promoter);
            this.Context.SaveChanges();
        }

        public void AddVisitor(ApplicationUser user)
        {
            Visitor visitor = new Visitor();
            ApplicationUser currentUser = this.Context.Users.Find(user.Id);
            visitor.User = currentUser;
            this.Context.Visitors.Add(visitor);
            this.Context.SaveChanges();
        }
    }
}