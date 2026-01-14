using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//MVC
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ Add authentication (Cookies)
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login/Logout";
    });

builder.Services.AddAuthorization();

// ✅ Add HttpClient for API calls


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();    // Must come before Authorization
app.UseAuthorization();
app.MapControllers(); // ✅ Maps [ApiController] routes

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
