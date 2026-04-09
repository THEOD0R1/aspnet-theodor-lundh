using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseTemple.Domain.Abstractions.Repositories;
using PulseTemple.Infrastructure.Persistence.EFC.Contexts;
using PulseTemple.Infrastructure.Persistence.EFC.Repositories.CustomerService;

namespace PulseTemple.Infrastructure.Persistence;

public static class PersistenceRegistrationExtension
{
    public static IServiceCollection  AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        services.AddEFCContext(configuration, env);

        services.AddScoped<IContactRequestRepository, ContactRequestRepository>();

        return services;
    }
}
