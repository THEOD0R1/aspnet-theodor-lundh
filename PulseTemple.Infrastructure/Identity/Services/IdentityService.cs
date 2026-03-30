using Microsoft.AspNetCore.Identity;
using PulseTemple.Application.Abstractions.Services;
using PulseTemple.Application.Abstractions.Users;
using PulseTemple.Application.Dtos;
using PulseTemple.Infrastructure.Persistence.Models;

namespace PulseTemple.Infrastructure.Identity.Services;

public class IdentityService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, RoleManager<RoleEntity> roleManager) : IIdentityService
{
    public Task<bool> DeleteByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> FindExistingEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetByEmailAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetByPhoneNumberAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDetails?> GetUserDetailsBydIdAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return null;

        return new UserDetails(
            Id: user.Id,
            MembershipId: user?.MembershipId,
            FirstName: user?.FirstName,
            LastName: user?.LastName,
            Email: user?.Email,
            PhoneNumber: user?.PhoneNumber,
            Membership: null 
            );
    }

    public Task<bool> LoginAsync(string email, string password, bool rememberMe, bool lockoutOnFailure = false)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<RegisterResult> RegisterAsync(string email, string password, string? roleName = "Member")
    {
        if (string.IsNullOrWhiteSpace(email))
            return new RegisterResult(false, ["Email address must be provided"]);

        if (string.IsNullOrWhiteSpace(password))
            return new RegisterResult(false, ["Password must be provided"]);

        var user = await userManager.FindByEmailAsync(email);

        if (user is not null)
            return new RegisterResult(false, ["User with same email address already exists"]);

        user = new UserEntity
        {
            UserName = email,
            Email = email,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            RowVersion = []
        };

        var created = await userManager.CreateAsync(user, password);

        if (!created.Succeeded)
            return new RegisterResult(false, ["Unable to create user"]);


        if(!string.IsNullOrWhiteSpace(roleName) && await roleManager.RoleExistsAsync(roleName) && !await userManager.IsInRoleAsync(user, roleName))
            await userManager.AddToRoleAsync(user, roleName);


        return new RegisterResult(true, ["User was created"], user.Id);
    }

    public Task<bool> UpdatePhoneNumberByIdAsync(Guid userId, string phoneNumber)
    {
        throw new NotImplementedException();
    }
}
