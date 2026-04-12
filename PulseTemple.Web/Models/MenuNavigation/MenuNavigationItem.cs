namespace PulseTemple.Web.Models.MenuNavigation;

public readonly record struct MenuNavigationItem(
    string ControllerName,
    string DisplayName,
    string? Url,
    IReadOnlyList<MenuNavigationChildItem> Children
    );
