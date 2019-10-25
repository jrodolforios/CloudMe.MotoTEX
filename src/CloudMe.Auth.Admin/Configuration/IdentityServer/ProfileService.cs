using CloudMe.ToDeTaxi.Infraestructure.Entries;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CloudMe.Auth.Admin.Configuration.IdentityServer
{
    public class ProfileService : IProfileService
    {
        protected UserManager<Usuario> _userManager;

        public ProfileService(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            var defaultClaims = await _userManager.GetClaimsAsync(user);
            context.IssuedClaims.AddRange(defaultClaims);

            var claims = new List<Claim>
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("nome", user.Nome),
                    new Claim("email", user.Email),
                };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = user != null;
        }
    }
}
