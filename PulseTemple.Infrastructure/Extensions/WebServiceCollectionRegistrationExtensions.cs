using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PulseTemple.Infrastructure.Extensions;

public  static class WebServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddWebSession(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = ".PulseTemple.Session";
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return services;
    }
}
