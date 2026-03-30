using System.ComponentModel.DataAnnotations;

namespace PulseTemple.Web.Models.Authentications.Register.Password;

public class SetPasswordForm : ConfirmPasswordInput
{
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
    ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character"
    )]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter Password")]
    public string Password { get; set; } = null!;
}
