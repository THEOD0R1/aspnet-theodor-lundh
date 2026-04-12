using System.ComponentModel.DataAnnotations;

namespace PulseTemple.Web.Models.Authentications.Register.Password;

public class ConfirmPasswordInput : UserTerms
{
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "Password must match")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
    public string ConfirmPassword { get; set; } = null!;
}
