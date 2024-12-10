using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using OAuthPOC.Models;
using OAuthPOC.Models.Interfaces;
using System.Security.Claims;

namespace OAuthPOC.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        public async Task<(bool Success, string Name, string Email)> AuthenticateAsync(HttpContext httpContext)
        {
            var result = await httpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                return (false, string.Empty, string.Empty);
            }

            var claims = result.Principal.Claims.ToList();
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return (true, name, email);
        }
    }
}
