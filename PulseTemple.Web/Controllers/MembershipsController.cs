using Microsoft.AspNetCore.Mvc;
using PulseTemple.Web.Attributes.MenuNavigation;

namespace PulseTemple.Web.Controllers;

[Route("[controller]")]

public class MembershipsController : Controller
{
    [MenuItem("Memberships", 1)]
    public IActionResult Index()
    {
        return View();
    }
}
