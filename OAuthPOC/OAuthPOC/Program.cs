using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using OAuthPOC.Models.Interfaces;
using OAuthPOC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFacebookAuthService, FacebookAuthService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = FacebookDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddFacebook(options =>
{
    options.AppId = "1127950965603605";
    options.AppSecret = "b3f4bc20c1920026f3850be0fe7de2e1";
    options.CallbackPath = "/signin-facebook";
    options.Scope.Add("email");
    options.Fields.Add("name");
})
.AddCookie();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();
    if (path.Contains("/swagger"))
    {
        context.Response.Redirect("/api/login/signin-facebook");
        return;
    }

    await next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
