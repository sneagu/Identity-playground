using System.Data.Entity;
using System.Threading.Tasks;
using ClassLibrary1.Infrastructure.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClassLibrary1.Infrastructure.Data.Repositories
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }

        public override async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            await base.SetPasswordHashAsync(user, passwordHash);

            if (!string.IsNullOrEmpty(passwordHash))
                await AddToPreviousPasswordsAsync(user, user.PasswordHash);
        }

        //public override async Task CreateAsync(ApplicationUser user)
        //{
        //    await base.CreateAsync(user);

        //    if(!string.IsNullOrEmpty(user.PasswordHash))
        //        await AddToPreviousPasswordsAsync(user, user.PasswordHash);
        //}

        public Task AddToPreviousPasswordsAsync(ApplicationUser user, string password)
        {
            user.PreviousUserPasswords.Add(new PreviousPassword() { UserId = user.Id, PasswordHash = password });
            return UpdateAsync(user);
        }
    }
}
