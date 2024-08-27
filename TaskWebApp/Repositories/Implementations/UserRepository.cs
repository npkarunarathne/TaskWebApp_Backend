using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskWebApp.Areas.Identity.Data;
using TaskWebApp.Data;
using TaskWebApp.Repositories.Interfaces;

namespace TaskWebApp.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TaskWebAppContext _context;

        public UserRepository(UserManager<IdentityUser> userManager, TaskWebAppContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityUser> FindByEmailAsync(string email) =>
            await _userManager.FindByEmailAsync(email);

        public async Task<IdentityUser> FindByIdAsync(string userId) =>
            await _userManager.FindByIdAsync(userId);

        public async Task<bool> CreateUserAsync(IdentityUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetUserRolesAsync(IdentityUser user) =>
            await _userManager.GetRolesAsync(user);

        public async Task AddRefreshTokenAsync(RefreshToken token)
        {
            await _context.RefreshToken.AddAsync(token);
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string token) =>
            await _context.RefreshToken.SingleOrDefaultAsync(x => x.Token == token);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }

  

}
