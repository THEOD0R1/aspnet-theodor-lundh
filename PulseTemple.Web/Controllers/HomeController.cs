using Microsoft.AspNetCore.Mvc;

namespace PulseTemple.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
