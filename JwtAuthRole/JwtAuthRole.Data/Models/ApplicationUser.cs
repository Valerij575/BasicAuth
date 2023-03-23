using Microsoft.AspNetCore.Identity;

namespace JwtAuthRole.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
