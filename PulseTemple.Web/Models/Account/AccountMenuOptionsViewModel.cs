namespace PulseTemple.Web.Models.Account;

public class AccountMenuOptionsViewModel
{
    public string Title { get; set; } = null!;
    public IReadOnlyList<AccountMenuOptionItem> AccountOptionItems { get; set; } = [];
}
