using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TribuneAPI.Data;

namespace TribuneAPI.Controllers
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _db;
        public LoginController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "admin123")
            {
                return Ok(new { message = "Login successful" });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "🚪 Logged out successfully" });
        }

        [HttpGet("CheckLogin")]
        public IActionResult CheckLogin()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Ok(new { message = $"✅ You are logged in as {User.Identity.Name}" });
            }
            return Unauthorized(new { message = "⚠️ Not logged in" });
        }
    }
}
