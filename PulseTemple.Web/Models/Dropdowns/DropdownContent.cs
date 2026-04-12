namespace PulseTemple.Web.Models.Dropdowns;

public record DropdownContent(
    string? Description = null,
    IReadOnlyCollection<string>? ContentList = null
);