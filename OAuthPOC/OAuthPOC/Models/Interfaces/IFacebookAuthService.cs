namespace OAuthPOC.Models.Interfaces
{
    public interface IFacebookAuthService
    {
        Task<(bool Success, string Name, string Email)> AuthenticateAsync(HttpContext httpContext);
    }
}
