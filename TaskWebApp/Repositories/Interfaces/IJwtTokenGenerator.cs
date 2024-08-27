using Microsoft.AspNetCore.Identity;
using TaskWebApp.Areas.Identity.Data.User;

namespace TaskWebApp.Repositories.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<AuthenticationResult> GenerateAuthenticationResultsForUser(IdentityUser user, IList<string> roles);
    }
}
