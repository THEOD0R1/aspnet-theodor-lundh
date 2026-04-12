namespace PulseTemple.Web.Models.Dropdowns;

public record DropdownItem(
    string? Number = null,
    string? Title = null,
    DropdownContent? Content = null
);
