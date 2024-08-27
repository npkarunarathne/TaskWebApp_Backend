using Microsoft.AspNetCore.Identity;
using TaskWebApp.Areas.Identity.Data;

namespace TaskWebApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<IdentityUser> FindByIdAsync(string userId);
        Task<bool> CreateUserAsync(IdentityUser user, string password);
        Task<IList<string>> GetUserRolesAsync(IdentityUser user);
        Task AddRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task SaveChangesAsync();
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
    }
}
