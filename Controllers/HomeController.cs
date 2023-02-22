using Microsoft.AspNetCore.Mvc;

namespace Geneanet.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Admin");
    }
}