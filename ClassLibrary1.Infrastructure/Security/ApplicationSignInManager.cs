using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace ClassLibrary1.Infrastructure.Security
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public async override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            // TODO: Check Password expiration
            var user = await UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                // NOTE: Use history to get latest pwd date
                //if (user.LastPasswordChangedDate.AddDays(90) < DateTime.Now)
                //    // user needs to change password
            }

            var result = await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

            //// NOTE: Add an audit record into audit table
            //// see http://stevejgordon.co.uk/extending-the-asp-net-core-identity-signinmanager
            //if (user != null) // We can only log an audit record if we can access the user object and it's ID
            //{
            //    var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            //    UserAudit auditRecord = null;

            //    switch (result.ToString())
            //    {
            //        case "Succeeded":
            //            auditRecord = UserAudit.CreateAuditEvent(appUser.Id, UserAuditEventType.Login, ip);
            //            break;

            //        case "Failed":
            //            auditRecord = UserAudit.CreateAuditEvent(appUser.Id, UserAuditEventType.FailedLogin, ip);
            //            break;
            //    }

            //    if (auditRecord != null)
            //    {
            //        _db.UserAuditEvents.Add(auditRecord);
            //        await _db.SaveChangesAsync();
            //    }
            //}

            return result;
        }

        // NOTE: Not available for override (see IAuthenticationManager)
        //// see http://stevejgordon.co.uk/extending-the-asp-net-core-identity-signinmanager
        //public override async Task SignOutAsync()
        //{
        //    await base.SignOutAsync();

        //    var user = await _userManager.FindByIdAsync(_contextAccessor.HttpContext.User.GetUserId()) as IdentityUser;

        //    if (user != null)
        //    {
        //        var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        //        var auditRecord = UserAudit.CreateAuditEvent(user.Id, UserAuditEventType.LogOut, ip);
        //        _db.UserAuditEvents.Add(auditRecord);
        //        await _db.SaveChangesAsync();
        //    }
        //}
    }
}