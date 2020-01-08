using CloudMe.ToDeTaxi.Infraestructure.Entries;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
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

            List<string> list = context.RequestedClaimTypes.ToList();

            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
            var defaultClaims = await _userManager.GetClaimsAsync(user);

            context.IssuedClaims.AddRange(defaultClaims);
            context.IssuedClaims.Add(new Claim("nome", user.Nome));
            context.IssuedClaims.AddRange(roleClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = user != null;
        }
    }
}
