using System.ComponentModel.DataAnnotations;

namespace PulseTemple.Web.Models.Authentications.Register;

public class UserTerms
{
    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions to proceed.")]
    [Display(Name = "Accept user terms and conditions")]
    public bool AcceptedUserTerm { get; set; }
}
