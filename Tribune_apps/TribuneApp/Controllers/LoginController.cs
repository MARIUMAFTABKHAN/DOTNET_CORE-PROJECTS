using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace TribuneApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBase = "http://localhost/TribuneAPI/api/Login/Login";

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            return View(model: returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(string username, string password, string? returnUrl)
        {
            var client = _httpClientFactory.CreateClient();

            var loginData = new
            {
                username = username,
                password = password
            };

            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(_apiBase, content);
                var resultContent = await response.Content.ReadAsStringAsync(); // 👈 capture raw content

                if (response.IsSuccessStatusCode)
                {
                    // ✅ Authenticate user using cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // ✅ Redirect safely
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = $"Invalid username or password. API responded with: {response.StatusCode}, Content: {resultContent}";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error connecting to API: " + ex.Message;
                return View("Login");
            }
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
