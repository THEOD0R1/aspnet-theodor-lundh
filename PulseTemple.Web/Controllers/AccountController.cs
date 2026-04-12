using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseTemple.Application.Abstractions.Services;
using PulseTemple.Application.Dtos.Identity;
using PulseTemple.Web.Attributes.MenuNavigation;
using PulseTemple.Web.Models.Account;
using System.Security.Claims;

namespace PulseTemple.Web.Controllers;

[Route("account")]
[Authorize]
public class AccountController(IIdentityService auth) : Controller
{
    [HideInMenu]
    public IActionResult Index() => RedirectToAction(nameof(AboutMe));

    [HideInMenu]
    [HttpGet("about-me")]
    public async Task<IActionResult> AboutMe()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bool parsedSucceeded = Guid.TryParse(userId, out Guid userIdGuid);


        if (!string.IsNullOrWhiteSpace(userId) && parsedSucceeded)
        {
            var account = await auth.GetUserDetailsBydIdAsync(userIdGuid);

            var viewModel = new AboutMeViewModel
            {
                AboutMeForm = new AboutMeForm
                {
                    FirstName = account?.FirstName ?? "",
                    LastName = account?.LastName ?? "",
                    Email = account?.Email ?? "",
                    PhoneNumber = account?.PhoneNumber ?? ""
                },
                ProfileImageUrl = account?.ImageUrl ?? "/images/image-avatar.png",

            };
            return View(viewModel);
        }

        await auth.LogoutAsync();
        return Redirect("/");
    }

    [HttpPost("about-me")]
    public async Task<IActionResult> AboutMe(AboutMeViewModel viewModel)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bool parsedSucceeded = Guid.TryParse(userId, out Guid userIdGuid);

        if (string.IsNullOrWhiteSpace(userId) || !parsedSucceeded)
            return RedirectToAction(nameof(SignOut));

        var account = await auth.GetUserDetailsBydIdAsync(userIdGuid);
        if (account is null)
            return RedirectToAction(nameof(SignOut));

        viewModel.ProfileImageUrl = account?.ImageUrl ?? "/images/image-avatar.png";

        if (!ModelState.IsValid)
            return View(viewModel);

        var imageUrl = account?.ImageUrl;

        if (viewModel.AboutMeForm.ProfileImage is not null && viewModel.AboutMeForm.ProfileImage.Length > 0)
        {
            imageUrl = await SaveProfileImageAsync(viewModel.AboutMeForm.ProfileImage);
        }

        var updatedAccountDetails = new UpdateAccountDetails(
            viewModel.AboutMeForm.Email,
            viewModel.AboutMeForm.FirstName,
            viewModel.AboutMeForm.LastName,
            viewModel.AboutMeForm.PhoneNumber,
            imageUrl
        );

        var result = await auth.UpdateBydIdAsync(userIdGuid, updatedAccountDetails);

        if (!result)
        {
            viewModel.ProfileImageUrl = imageUrl ?? "/images/image-avatar.png";
            viewModel.StatusMessage = "Unable to save changes";
            return View(viewModel);
        }

        return RedirectToAction(nameof(AboutMe));
    }


    [HideInMenu]
    [HttpGet("sign-out")]
    public new async Task<IActionResult> SignOut()
    {
        await auth.LogoutAsync();
        return Redirect("/");
    }

    [HideInMenu]
    [HttpGet("remove-account")]
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

    private static async Task<string> SaveProfileImageAsync(IFormFile file)
    {
        Console.WriteLine("hej ja");
        
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
        Directory.CreateDirectory(uploadsFolder);

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/profiles/{fileName}";
    }
}
