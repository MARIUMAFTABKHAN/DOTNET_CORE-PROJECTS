using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class HomeController : Controller
{

    //public IActionResult Index() => View();

    public IActionResult Index()
    {
        Console.WriteLine("🔵 HomeController Index() hit: " + User.Identity?.Name);
        return View();
    }
}
