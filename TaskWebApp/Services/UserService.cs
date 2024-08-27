using Microsoft.AspNetCore.Identity;
using TaskWebApp.Areas.Identity.Data.User;
using TaskWebApp.Repositories.Interfaces;
using TaskWebApp.Services.Interfaces;

namespace TaskWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleManager = roleManager;
        }

        public async Task<string> RegisterUserAsync(UserRegistration request)
        {
            var existingUser = await _userRepository.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new Exception("User with this Email already exists");
            }

            var newUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.UserName,
            };

            if (!await _roleManager.RoleExistsAsync(request.Type))
            {
                await _roleManager.CreateAsync(new IdentityRole(request.Type));
            }

            var created = await _userRepository.CreateUserAsync(newUser, request.Password);
            if (!created)
            {
                throw new Exception("User creation failed");
            }

            return "Registration successful";
        }

        public async Task<UserLoginResults> LoginUserAsync(UserLogin request)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);

            if (user == null || !await _userRepository.CheckPasswordAsync(user, request.Password))
            {
                throw new Exception("Invalid credentials");
            }

            var roles = await _userRepository.GetUserRolesAsync(user);
            var tokens = await _jwtTokenGenerator.GenerateAuthenticationResultsForUser(user, roles);

            return new UserLoginResults
            {
                User = user,
                Role = roles,
                Token = tokens.Token,
                RefreshToken = tokens.RefreshToken,
                TokenExpireDate = tokens.TokenExpireDate,
            };
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var storedToken = await _userRepository.GetRefreshTokenAsync(request.RefreshToken);

            if (storedToken == null || !storedToken.IsValid)
            {
                throw new Exception("Invalid refresh token");
            }

            storedToken.Revoked = DateTime.UtcNow;
            storedToken.Used = true;
            await _userRepository.SaveChangesAsync();

            var user = await _userRepository.FindByIdAsync(storedToken.UserId);
            var roles = await _userRepository.GetUserRolesAsync(user);
            return await _jwtTokenGenerator.GenerateAuthenticationResultsForUser(user, roles);
        }
    }

}
