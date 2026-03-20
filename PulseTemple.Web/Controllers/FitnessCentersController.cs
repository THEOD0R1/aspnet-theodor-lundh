using Microsoft.AspNetCore.Mvc;
using PulseTemple.Web.Attributes.MenuNavigation;

namespace PulseTemple.Web.Controllers;

[Route("[controller]")]
public class FitnessCentersController : Controller
{
    [MenuItem("Fitness Centers", 1)]
    public IActionResult Index()
    {
        return View();
    }
}
