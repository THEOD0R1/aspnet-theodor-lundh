using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseTemple.Application.Dtos.CustomerService.ContactRequests;

namespace PulseTemple.Application.Extensions;

public static class ApplicationServiceCollectionRegistrationExtenstions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        services.AddScoped<IContactRequestService, ContactRequestService>();

        return services;
    }
}
