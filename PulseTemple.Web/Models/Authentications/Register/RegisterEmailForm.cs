using System.ComponentModel.DataAnnotations;

namespace PulseTemple.Web.Models.Authentications.Register;

public class RegisterEmailForm
{
    [Required(ErrorMessage = "Email address is required")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter Email Address")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address", Prompt = "username@example.com")]
    public string Email { get; set; } = null!;
}
