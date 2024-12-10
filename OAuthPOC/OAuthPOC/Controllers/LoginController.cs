using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Mvc;
using OAuthPOC.Models.Interfaces;

namespace OAuthPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IFacebookAuthService _facebookAuthService;

        public LoginController(IFacebookAuthService facebookAuthService)
        {
            _facebookAuthService = facebookAuthService;
        }

        [HttpGet("signin-facebook")]
        public IActionResult FacebookLogin()
        {
            var redirectUrl = Url.Action("FacebookCallback", "Login");
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };

            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet("facebook-callback")]
        public async Task<IActionResult> FacebookCallback()
        {
            var (success, name, email) = await _facebookAuthService.AuthenticateAsync(HttpContext);

            if (!success)
            {
                return Unauthorized("Facebook authentication failed.");
            }


            return Ok(new { Name = name, Email = email });
        }
    }
}
