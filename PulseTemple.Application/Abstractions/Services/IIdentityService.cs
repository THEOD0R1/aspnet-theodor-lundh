using PulseTemple.Application.Abstractions.Users;
using PulseTemple.Application.Dtos.Identity;
using PulseTemple.Application.Dtos.Results;

namespace PulseTemple.Application.Abstractions.Services;

public interface IIdentityService
{
    Task<RegisterResult> RegisterAsync(string email, string password, string? roleName = "Member");
    Task<UserDetails?> GetUserDetailsBydIdAsync(Guid id);
    Task<bool> LoginAsync(string email, string password, bool rememberMe, bool lockoutOnFailure = false);
    Task LogoutAsync();

    Task<string?> GetByEmailAsync(Guid userId);
    Task<string?> GetByPhoneNumberAsync(Guid userId);
    
    Task<bool> DeleteByIdAsync(Guid userId);
    Task<bool> FindExistingEmailAsync(string email);
    Task<bool> UpdateBydIdAsync(Guid userId, UpdateAccountDetails accountDetails);
}
