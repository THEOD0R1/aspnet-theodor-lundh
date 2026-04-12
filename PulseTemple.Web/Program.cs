using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PulseTemple.Application.Extensions;
using PulseTemple.Infrastructure.Extensions;
using PulseTemple.Infrastructure.Persistence;
using PulseTemple.Web.Routing;
using PulseTemple.Web.Services.MenuNavigation;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseStaticWebAssets();

builder.Services.AddControllersWithViews(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new MenuUrlRewriter())));

builder.Services.AddRouting(options =>
    options.LowercaseUrls = true);

builder.Services.AddControllersWithViews();

builder.Services.AddApplication(builder.Configuration, builder.Environment);
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/sign-in";
    options.LogoutPath = "/";
    options.AccessDeniedPath = "/error/accessdenied";

    options.Cookie.Name = "PulseTemple.Auth";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;

    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddScoped<IMenuNavigationService, MenuNavigationService>();

var app = builder.Build();

await PersistenceInitializer.InitializeAsync(app.Services, app.Environment);

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
