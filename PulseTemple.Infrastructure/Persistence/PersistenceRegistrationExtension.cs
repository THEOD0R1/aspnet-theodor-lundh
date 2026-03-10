using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseTemple.Infrastructure.Persistence.EFC.Contexts;

namespace PulseTemple.Infrastructure.Persistence;

public static class PersistenceRegistrationExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        services.AddEFCContext(configuration, env);

        return services;
    }
}
