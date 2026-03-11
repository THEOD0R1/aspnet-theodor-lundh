using Microsoft.AspNetCore.Mvc.ApplicationModels;
using PulseTemple.Infrastructure.Extensions;
using PulseTemple.Infrastructure.Persistence;
using PulseTemple.Web.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    options.Conventions.Add(new RouteTokenTransformerConvention(new MenuUrlRewriter())));

builder.Services.AddRouting(options =>
    options.LowercaseUrls = true);

builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

await PersistenceInitializer.InitializeAsync(app.Services, app.Environment);

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
