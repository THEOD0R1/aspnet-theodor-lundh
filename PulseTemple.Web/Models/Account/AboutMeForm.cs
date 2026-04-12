using System.ComponentModel.DataAnnotations;

namespace PulseTemple.Web.Models.Account;

public sealed class AboutMeForm
{
    [Required(ErrorMessage = "First name is required")]
    [DataType(DataType.Text)]
    [Display(Name = "First Name", Prompt = "Enter First Name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required")]
    [DataType(DataType.Text)]
    [Display(Name = "Last Name", Prompt = "Enter Last Name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address", Prompt = "username@domain.com")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Phone number must be valid")]
    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number", Prompt = "Enter Phone Number")]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.Upload)]
    [Display(Name = "Profile Image", Prompt = "Upload Profile Image")]
    public IFormFile? ProfileImage { get; set; }
}
