using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Identity.data;

internal class IdentityInitializer
{
    public static async Task InitilizeDefaultRolesAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

        List<RoleEntity> roles = [
            new RoleEntity{
                Name = "Member"
            },
            new RoleEntity{
                Name = "Admin"
            }
        ];

        if (roleManager is null) return;

        foreach (var role in roles)
            if (!string.IsNullOrWhiteSpace(role.Name) && !await roleManager.RoleExistsAsync(role.Name))
                await roleManager.CreateAsync(role);
    }

    public static async Task InitilizeDefaultAdminAccountsAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();


        var adminEmail = "admin@test.com";

        List<UserEntity> users = [
            new UserEntity {
            Id = Guid.NewGuid(),
            UserName = adminEmail,
            Email = adminEmail,
            NormalizedEmail = adminEmail.ToUpperInvariant(),
            NormalizedUserName = adminEmail.ToUpperInvariant(),
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            EmailConfirmed = true,
            RowVersion = []
            }
        ];
        
        if (userManager is null || await userManager.FindByEmailAsync(adminEmail) is not null) return;

        var defaultPassword = "Test123!";
        var defaultRoleName = "Admin";

        foreach (var user in users)
        {
            var created = await userManager.CreateAsync(user, defaultPassword);

            if (!created.Succeeded)
                    foreach (var error in created.Errors)
                        Console.WriteLine($"Error: {error.Description}");

            if (roleManager is not null && created.Succeeded && await roleManager.RoleExistsAsync(defaultRoleName))
                        await userManager.AddToRoleAsync(user, defaultRoleName);
        }

    }
    public static async Task InitilizeDefaultMemberAccountsAsync(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();


        var memberEmail = "member@test.com";

        List<UserEntity> users = [
            new UserEntity {
            Id = Guid.NewGuid(),
            UserName = memberEmail,
            Email = memberEmail,
            NormalizedEmail = memberEmail.ToUpperInvariant(),
            NormalizedUserName = memberEmail.ToUpperInvariant(),
            SecurityStamp = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            EmailConfirmed = true,
            RowVersion = []
            }
        ];

        if (userManager is null || await userManager.FindByEmailAsync(memberEmail) is not null) return;

        var defaultPassword = "Test123!";
        var defaultRoleName = "Member";

        foreach (var user in users)
        {
            var created = await userManager.CreateAsync(user, defaultPassword);

            if (!created.Succeeded)
                foreach (var error in created.Errors)
                    Console.WriteLine($"Error: {error.Description}");

            if (roleManager is not null && created.Succeeded && await roleManager.RoleExistsAsync(defaultRoleName))
                await userManager.AddToRoleAsync(user, defaultRoleName);
        }

    }
}
