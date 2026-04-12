using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseTemple.Application.Abstractions.Services;
using PulseTemple.Application.Abstractions.Users;
using PulseTemple.Web.Attributes.MenuNavigation;
using PulseTemple.Web.Models.Authentications.Register;
using PulseTemple.Web.Models.Authentications.Register.Password;

namespace PulseTemple.Web.Controllers;


public class AuthenticationController(IIdentityService auth) : Controller
{
    private const string RegisterEmailSessionKey = "RegisterEmailAddress";

    [HideInMenu]
    [HttpGet("sign-up")]
    [AllowAnonymous]
    public IActionResult SignUp()
    {
        var redirect = RedirectWhenLoggedIn;

        if (redirect != null)
            return redirect;

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
    [AllowAnonymous]
    [HttpGet("set-password")]
    public IActionResult SetPassword()
    {
        var email = HttpContext.Session.GetString(RegisterEmailSessionKey);
        if (string.IsNullOrWhiteSpace(email))
            return RedirectToAction(nameof(SignUp));

        return View();
    }

    [HttpPost("set-password")]
    [AllowAnonymous]
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

        return RedirectToAction(nameof(SignIn));
    }

    [HideInMenu]
    [AllowAnonymous]
    [HttpGet("sign-in")]
    public IActionResult SignIn()
    {
        var redirect = RedirectWhenLoggedIn;

        if (redirect != null)
            return redirect;

        return View();
    }

    [HttpPost("sign-in")]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(LoginForm form)
    {
        if (!ModelState.IsValid)
        {
            ViewData["ErrorMessage"] = "Incorrect email or password";
            return View(form);
        }

        var loggedIn = await auth.LoginAsync(form.Email, form.Password, form.RememberMe, true);
        if (!loggedIn)
        {
            ViewData["ErrorMessage"] = "Incorrect email or password";
            return View(form);
        }
        
        return RedirectToAction("Index", "Account");
    }
    [HttpPost]
    public new async Task<IActionResult> SignOut()
    {
        await auth.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    private IActionResult? RedirectWhenLoggedIn
    {
        get
        {
            Console.WriteLine("test:" + User.Identity?.IsAuthenticated);
            if (User.Identity?.IsAuthenticated == false) return null;
            
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "AdminDashboard");

            if (User.IsInRole("Member"))
                return RedirectToAction("Index", "Account");

            return null;
        }
    }

}
