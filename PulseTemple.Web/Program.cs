using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PulseTemple.Infrastructure.Extensions;
using PulseTemple.Infrastructure.Persistence;
using PulseTemple.Web.Routing;
using PulseTemple.Web.Services.MenuNavigation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new MenuUrlRewriter())));

builder.Services.AddRouting(options =>
    options.LowercaseUrls = true);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "PulseTemple.Auth";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
    
    options.LoginPath = "/authentication/signin";
    options.AccessDeniedPath = "/error/accessdenied";
    options.LogoutPath = "/";
});

builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
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
