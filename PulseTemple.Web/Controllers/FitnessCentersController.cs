using Microsoft.AspNetCore.Mvc;

namespace PulseTemple.Web.Controllers;

[Route("[controller]")]
public class FitnessCentersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
