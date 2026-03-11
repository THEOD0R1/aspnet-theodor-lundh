using Microsoft.AspNetCore.Mvc;

namespace PulseTemple.Web.Controllers;

public class TrainingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
