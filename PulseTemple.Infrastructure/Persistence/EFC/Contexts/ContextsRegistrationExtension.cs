using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseTemple.Infrastructure.Extensions.Models;
using Microsoft.AspNetCore.Identity;

namespace PulseTemple.Infrastructure.Extensions.EFC.Contexts;

public static class ContextsRegistrationExtension
{
    public static IServiceCollection AddEFCContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        if (env.IsDevelopment())
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
        })
        .AddRoles<RoleEntity>()
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
