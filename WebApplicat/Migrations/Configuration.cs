using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplicat.Models;
using System.Linq;

namespace WebApplicat.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebApplicat.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Mod"))
            {
                roleManager.Create(new IdentityRole { Name = "Mod" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "alancravey@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "alancravey@gmail.com",
                    Email = "alancravey@gmail.com",
                    FirstName = "Alan",
                    LastName = "Cravey",
                    DisplayName = "Alan Cravey",
                }, "Bortles05!!");
            }
            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Coderfoundry",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "Coder",
                    LastName = "Foundry",
                    DisplayName = "Coder Foundry",
                }, "Password-1");
            }
            var userId = userManager.FindByEmail("alancravey@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            var auserId = userManager.FindByEmail("moderator@coderfoundry.com").Id;
            userManager.AddToRole(auserId, "Mod");
        }
    }
}
