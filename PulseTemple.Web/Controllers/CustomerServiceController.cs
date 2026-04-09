using Microsoft.AspNetCore.Mvc;
using PulseTemple.Application.Dtos.CustomerService.ContactRequests;
using PulseTemple.Application.Dtos.CustomerService.ContactRequests.Inputs;
using PulseTemple.Web.Attributes.MenuNavigation;
using PulseTemple.Web.Models.CustomerService;

namespace PulseTemple.Web.Controllers;

[Route("[controller]")]
public class CustomerServiceController(IContactRequestService crService) : Controller
{
    [HttpGet]
    [MenuItem("Customer Service", 4)]
    public IActionResult Index()
    {
        var viewModel = new ContactRequestForm();
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactRequestForm form, CancellationToken ct = default)
    {
        if(!ModelState.IsValid)
            return View(form);

        var input = new ContactRequestInput(
            form.FirstName,
            form.LastName,
            form.Email,
            form.Phone,
            form.Message
            );

        var result = await crService.CreateContactRequestAsync(input, ct);

        TempData["ContactFormMessage"] = result.Success
           ? "Your message has been sent."
           : "Your message could not be sent. Please try again later.";

        return RedirectToAction(nameof(Index));
    }
}
