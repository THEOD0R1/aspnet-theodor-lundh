using PulseTemple.Web.Models.MenuNavigation;

namespace PulseTemple.Web.Services.MenuNavigation;

public interface IMenuNavigationService
{
    IReadOnlyList<MenuNavigationItem> GetMenu();
}
