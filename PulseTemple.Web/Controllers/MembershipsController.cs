using Microsoft.AspNetCore.Mvc;

namespace PulseTemple.Web.Controllers;

[Route("[controller]")]

public class MembershipsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
