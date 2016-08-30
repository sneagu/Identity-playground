using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClassLibrary1.Infrastructure.Security
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
            PreviousUserPasswords = new List<PreviousPassword>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public int ClientId { get; set; }

        public virtual IList<PreviousPassword> PreviousUserPasswords { get; set; }

    }
}