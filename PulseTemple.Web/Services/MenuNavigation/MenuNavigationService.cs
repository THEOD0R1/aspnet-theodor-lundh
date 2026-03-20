using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using PulseTemple.Web.Attributes.MenuNavigation;
using PulseTemple.Web.Models.MenuNavigation;

namespace PulseTemple.Web.Services.MenuNavigation;

public class MenuNavigationService(IActionDescriptorCollectionProvider actionProvider, LinkGenerator linkGenerator) : IMenuNavigationService
{
    public IReadOnlyList<MenuNavigationItem> GetMenu()
    {
        var actions = actionProvider.ActionDescriptors.Items
           .OfType<ControllerActionDescriptor>()
           .Where(IsVisibleInMenu)
           .Select(action => new MenuNavigationActionItem(
           
                action.ControllerName,
                action.ActionName,
                GetDisplayName(action),
                GetOrder(action),
                linkGenerator.GetPathByAction(
                   action: action.ActionName,
                   controller: action.ControllerName)
           ))
           .Where(x => !string.IsNullOrWhiteSpace(x.Url))
           .GroupBy(x => x.ControllerName)
           .OrderBy(g => g.Min(x => x.Order))
           .ThenBy(g => g.Key)
           .Select(group =>
           {
               var children = group
                   .OrderBy(x => x.Order)
                   .ThenBy(x => x.DisplayName)
                   .Select(x => new MenuNavigationChildItem
                   (
                       x.ControllerName,
                       x.ActionName,
                       x.DisplayName,
                       x.Url!
                   ))
                   .ToList();

               return new MenuNavigationItem(
                   group.Key,
                   GetControllerDisplayName(group.Key, children),
                   children.Count == 1 ? children[0].Url : null,
                   children
               );
           })
           .ToList();

        return actions;
    }

    private static bool IsVisibleInMenu(ControllerActionDescriptor action)
    {
        if (action.MethodInfo.GetCustomAttributes(typeof(NonActionAttribute), inherit: true).Length != 0)
            return false;

        if (action.MethodInfo.GetCustomAttributes(typeof(HideInMenuAttribute), inherit: true).Length != 0)
            return false;

        if (action.MethodInfo.GetParameters().Length > 0)
            return false;

        var httpMethodAttributes = action.MethodInfo
            .GetCustomAttributes(inherit: true)
            .OfType<HttpMethodAttribute>().ToList();

        if (httpMethodAttributes.Count != 0)
        {
            var allowedMethods = httpMethodAttributes
                .SelectMany(x => x.HttpMethods)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (!allowedMethods.Contains("GET", StringComparer.OrdinalIgnoreCase))
                return false;
        }

        return true;
    }
    private static string GetDisplayName(ControllerActionDescriptor action)
    {
        var menuItemAttribute = action.MethodInfo
            .GetCustomAttributes(typeof(MenuItemAttribute), inherit: true)
            .OfType<MenuItemAttribute>()
            .FirstOrDefault();

        return menuItemAttribute?.Title ?? action.ActionName;
    }
    private static int GetOrder(ControllerActionDescriptor action)
    {
        var menuItemAttribute = action.MethodInfo
            .GetCustomAttributes(typeof(MenuItemAttribute), inherit: true)
            .OfType<MenuItemAttribute>()
            .FirstOrDefault();

        return menuItemAttribute?.Order ?? 1000;
    }
    private static string GetControllerDisplayName(string controllerName, List<MenuNavigationChildItem> children)
    {
        if (children.Count == 1)
            return children[0].DisplayName;

        return controllerName;
    }
}
