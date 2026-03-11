namespace PulseTemple.Web.Models.MenuNavigation;

public readonly record struct MenuNavigationActionItem(
    string ControllerName,
    string ActionName,
    string DisplayName,
    int Order,
    string? Url
);