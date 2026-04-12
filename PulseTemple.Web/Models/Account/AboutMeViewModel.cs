namespace PulseTemple.Web.Models.Account;

public class AboutMeViewModel
{
    public string? ProfileImageUrl { get; set; }
    public AboutMeForm AboutMeForm { get; set; } = new AboutMeForm();
    public string? StatusMessage { get; set; }
}
