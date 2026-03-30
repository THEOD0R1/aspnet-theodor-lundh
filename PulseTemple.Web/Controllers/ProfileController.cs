using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseTemple.Application.Abstractions.Services;
using PulseTemple.Web.Attributes.MenuNavigation;
using System.Security.Claims;

namespace PulseTemple.Web.Controllers;

[Authorize]
public class ProfileController(IIdentityService auth) : Controller
{
    [HideInMenu]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> RemoveAccount()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        bool parsedSucceeded = Guid.TryParse(userId, out Guid userIdGuid);

        if (string.IsNullOrEmpty(userId) || !parsedSucceeded)
            return Unauthorized();

        var result = await auth.DeleteByIdAsync(userIdGuid);

        if (result)
        {
            await auth.LogoutAsync();
            RedirectToAction("Index", "Home");
        }

        return RedirectToAction(nameof(Index));

    }
}
