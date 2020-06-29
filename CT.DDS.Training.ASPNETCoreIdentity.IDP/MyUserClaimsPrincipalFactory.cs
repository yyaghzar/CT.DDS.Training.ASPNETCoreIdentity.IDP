using CT.DDS.Training.ASPNETCoreIdentity.IDP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CT.DDS.Training.ASPNETCoreIdentity.IDP
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Title", user.Title));
            return identity;
        }
    }
}
