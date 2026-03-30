using System.ComponentModel.DataAnnotations;

namespace PulseTemple.Web.Models.Authentications.Register;

public class LoginForm : RegisterEmailForm
{
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Remeber me")]
    public bool RememberMe { get; set; }
}
