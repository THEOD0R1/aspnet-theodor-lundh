using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseTemple.Application.Abstractions.Services;
using PulseTemple.Application.Abstractions.Users;
using PulseTemple.Web.Attributes.MenuNavigation;
using PulseTemple.Web.Models.Authentications.Register;

namespace PulseTemple.Web.Controllers;

public class AuthenticationController(IIdentityService auth) : Controller
{
    private const string RegisterEmailSessionKey = "RegisterEmailAddress";

    [HideInMenu]
    [HttpGet("sign-up")]
    [AllowAnonymous]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public IActionResult SignUp(RegisterEmailForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        HttpContext.Session.SetString(RegisterEmailSessionKey, form.Email);
        return RedirectToAction(nameof(SetPassword));
    }

    [HideInMenu]
    [HttpGet("set-password")]
    public IActionResult SetPassword()
    {
        var email = HttpContext.Session.GetString(RegisterEmailSessionKey);
        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(SignUp));

        return View();
    }

    [HttpPost("set-password")]
    public async Task<IActionResult> SetPassword(SetPasswordForm form)
    {
        var email = HttpContext.Session.GetString(RegisterEmailSessionKey);
        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(SignUp));

        if (!ModelState.IsValid)
            return View(form);

        var req = new RegisterUserRequest(email, form.Password);
        var result = await auth.RegisterAsync(req.Email, req.Password);
        
        if (!result.Succeded)
        {
            ViewData["ErrorMessage"] = result.Errors.FirstOrDefault();
            return View(form);
        }

        Console.Write(result);

        return RedirectToAction(nameof(SignIn));
    }

    [HideInMenu]
    [HttpGet("sign-in")]
    public IActionResult SignIn()
    {
        return View();
    }

}
