using JwtAuthRole.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthRole.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return (0, "Invalid user name or password");
            }
            if(!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return (0, "Invalid user name or password");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            string token = GenerateToken(authClaims);
            return (1, token);
        }

        public async Task<(int, string)> Registration(RegistrationModel model, string role)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if(userExists != null)
            {
                return (0, "User already exists");
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Name = model.UserName
            };

            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if(!createUserResult.Succeeded)
            {
                return (0, "User creation faild! Please check user details and try again");
            }

            if(!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if(await _roleManager.RoleExistsAsync(UserRole.User))
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return (1, "User created successfully!");
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authStringKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:ValidIssuer"],
                Audience = _configuration["Jwt:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authStringKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
