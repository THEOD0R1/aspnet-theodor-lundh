using Microsoft.AspNetCore.Mvc;
using PulseTemple.Web.Attributes.MenuNavigation;

namespace PulseTemple.Web.Controllers;

public class HomeController : Controller
{
    [HideInMenu]
    public IActionResult Index()
    {
        return View();
    }
}
