using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EventsApp.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EventsAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EventsAppContext context)
        {
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Admin");
                var roleCreateResult = roleManager.Create(role);
                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleCreateResult.Errors));
                }
            }

            if (!context.Roles.Any(role => role.Name == "Promoter"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Promoter");
                var roleCreateResult = roleManager.Create(role);
                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleCreateResult.Errors));
                }
            }

            if (!context.Roles.Any(role => role.Name == "Visitor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole("Visitor");
                var roleCreateResult = roleManager.Create(role);
                if (!roleCreateResult.Succeeded)
                {
                    throw new Exception(string.Join("; ", roleCreateResult.Errors));
                }
            }

        }
    }
}
