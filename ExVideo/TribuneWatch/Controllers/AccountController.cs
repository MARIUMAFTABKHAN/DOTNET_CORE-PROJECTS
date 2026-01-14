using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading.Channels;


[Route("Account")]
public class AccountController : Controller
{
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db)
    {
        _db = db;
    }


    [AllowAnonymous]
    [HttpGet("/login")]
    [HttpGet("Login")]
    public IActionResult Login(string? returnUrl = "/") => View(model: returnUrl ?? "/");

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginPost(string username, string password, string? returnUrl = null)
    {

        if (string.Equals(username, "admin", StringComparison.OrdinalIgnoreCase) && password == "admin123")
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            };
            var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(id);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return LocalRedirect($"{HttpContext.Request.PathBase}/Home/Index");
        }

        ViewBag.Error = "Invalid credentials";
        return View("Login");

        // Lookup user from imported table
        //var user = await _db.Users.FirstOrDefaultAsync(u =>
        //    u.UserName == username &&
        //    u.Password == password &&
        //    u.IsActive == true);

        //if (user != null)
        //{
        //    var claims = new[] {
        //    new Claim(ClaimTypes.Name, user.UserName),
        //    new Claim(ClaimTypes.Role, user.RoleId.ToString())

        //};
        //    var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        //    Console.WriteLine($"✅ Login successful for {username}");
        //    return Redirect("/Home/Index");
        //}

        //ViewBag.Error = "Invalid credentials";
        //return View("Login", returnUrl ?? "/");

    }



    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return LocalRedirect($"{HttpContext.Request.PathBase}/login");
    }
}