using System.ComponentModel.DataAnnotations;

namespace PulseTemple.Web.Models.CustomerService;

public sealed class ContactRequestForm
{
    [Required(ErrorMessage = "You must enter a first name")]
    [Display(Name = "First Name", Prompt = "Enter your first name")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Must contain at lest {2} characters")]
    public string FirstName { get; set;  } = null!;

    [Required(ErrorMessage = "You must enter a last name")]
    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Must contain at lest {2} characters")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "You must enter an email address")]
    [EmailAddress(ErrorMessage = "You must enter a valid email address")]
    [Display(Name = "Email Address", Prompt = "Enter your email address")]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone Number", Prompt = "Enter your phone number")]
    [Phone(ErrorMessage = "You must enter a valid phone number")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "You must enter a message")]
    [StringLength(4000, MinimumLength = 5, ErrorMessage = "Your message must be at least {2} characters long")]
    [Display(Name = "Message", Prompt = "Enter your message")]
    public string Message { get; set; } = null!;
}
