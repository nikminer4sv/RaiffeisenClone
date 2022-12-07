using System.Security.Claims;
using IdentityServer.WebApi.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.WebApi.Services;

public class ProfileService : IProfileService
{
    protected UserManager<AppUser> _userManager;

    public ProfileService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        //>Processing
        var user = await _userManager.GetUserAsync(context.Subject);

        var claims = new List<Claim>
        {
            new Claim("id", user.Id),
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        //>Processing
        var user = await _userManager.GetUserAsync(context.Subject);

        context.IsActive = (user != null);
    }
}