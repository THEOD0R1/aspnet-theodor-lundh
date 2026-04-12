namespace PulseTemple.Web.Models.Account;

public sealed class AccountMenuOptionItem
{
    public string Name { get; set; } = null!;
    public string Action { get; set; } = null!;
    public string Controller { get; set; } = null!;
    public string? Area { get; set; } 

    public string? ExternalUrl { get; set; }
}
