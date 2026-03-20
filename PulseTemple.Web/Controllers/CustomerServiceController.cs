using Microsoft.AspNetCore.Mvc;
using PulseTemple.Web.Attributes.MenuNavigation;

namespace PulseTemple.Web.Controllers;

public class CustomerServiceController : Controller
{
    [HttpGet]
    [MenuItem("Customer Service", 4)]
    public IActionResult Index()
    {
        return View();
    }
}
