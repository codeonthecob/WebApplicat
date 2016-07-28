using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplicat.Models;
using WebApplicat.Models.CodeFirst;

namespace WebApplicat.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public ApplicationUser()
        {
            this.Comments = new HashSet<Comment>();
        }
        public virtual ICollection<Comment> Comments { get; set; }
        public string Hometown { get; internal set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
         : base("DefaultConnection", throwIfV1Schema: false) { }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public IEnumerable ApplicationUsers { get; internal set; }
    }
}