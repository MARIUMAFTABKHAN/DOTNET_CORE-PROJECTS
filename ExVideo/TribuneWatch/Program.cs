using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using TribuneWatch.Data;

var builder = WebApplication.CreateBuilder(args);


var pathBase = "/WebSites/Tribune_Publish";

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("TribuneWatchContextConnection")));

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = $"{pathBase}/login";
        opt.AccessDeniedPath = $"{pathBase}/login";
        opt.LogoutPath = $"{pathBase}/Account/Logout";
        opt.Cookie.Path = pathBase;                   // 🔥 CRITICAL FIX
        opt.SlidingExpiration = true;
        opt.ExpireTimeSpan = TimeSpan.FromHours(8);
        opt.Cookie.SameSite = SameSiteMode.Lax;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

builder.Services.AddAuthorization();

builder.Services.Configure<TribuneWatch.Data.MediaOptions>(
    builder.Configuration.GetSection("MediaOptions"));

var app = builder.Build();

app.UsePathBase(pathBase);

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapGet("/", ctx =>
{
    ctx.Response.Redirect($"{pathBase}/login");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
