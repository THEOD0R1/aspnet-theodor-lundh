namespace PulseTemple.Web.Attributes.MenuNavigation;

public sealed class MenuItemAttribute(string title, int order = 1000)
{
    public string Title { get; } = title;
    public int Order { get; } = order;
}
