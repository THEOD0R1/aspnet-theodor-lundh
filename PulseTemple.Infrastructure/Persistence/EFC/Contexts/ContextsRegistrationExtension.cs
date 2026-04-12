using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseTemple.Application.Abstractions.Services;
using PulseTemple.Infrastructure.Identity.Services;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Persistence.EFC.Contexts;

public static class ContextsRegistrationExtension
{
    public static IServiceCollection AddEFCContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        bool isMigration = AppDomain.CurrentDomain.GetAssemblies()
            .Any(a => a.FullName?.Contains("Microsoft.EntityFrameworkCore.Design") == true);

        if (env.IsDevelopment() && !isMigration)
            services.AddDbContext<DataContext>(options =>
                options.UseInMemoryDatabase("PulseTempleDevDb"));
        else
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PulseTempleDb")));

        services.AddIdentity<UserEntity, RoleEntity>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddRoles<RoleEntity>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
