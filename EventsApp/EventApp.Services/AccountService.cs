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
            this.Context.UserInfos.Add(new PromoterInfo()
            {
                Contacts = bind.Contacts,
                Description = bind.Description,
                Name = bind.Name,
                Promoter = promoter
            });
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
    }
}