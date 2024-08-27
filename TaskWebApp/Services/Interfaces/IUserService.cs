using TaskWebApp.Areas.Identity.Data.User;

namespace TaskWebApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(UserRegistration request);
        Task<UserLoginResults> LoginUserAsync(UserLogin request);
        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
