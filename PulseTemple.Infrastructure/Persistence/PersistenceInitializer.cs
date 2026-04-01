using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PulseTemple.Infrastructure.Identity.data;
using PulseTemple.Infrastructure.Persistence.EFC.Contexts;

namespace PulseTemple.Infrastructure.Persistence;

public static class PersistenceInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, IHostEnvironment env, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(env);

        if (env.IsDevelopment())
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            await context.Database.EnsureCreatedAsync(ct);
        }
        else
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync(ct);
        }

        await IdentityInitializer.InitilizeDefaultRolesAsync(serviceProvider);
        await IdentityInitializer.InitilizeDefaultAdminAccountsAsync(serviceProvider);
        await IdentityInitializer.InitilizeDefaultMemberAccountsAsync(serviceProvider);
    }
}
