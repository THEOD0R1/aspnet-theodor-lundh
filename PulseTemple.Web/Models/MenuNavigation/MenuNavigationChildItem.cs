namespace PulseTemple.Web.Models.MenuNavigation;

public readonly record struct MenuNavigationChildItem(
    string ControllerName,
    string ActionName,
    string DisplayName,
    string? Url
    );
