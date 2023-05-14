using Microsoft.AspNetCore.Identity;

namespace api.Models.Domain;

public class User: IdentityUser
{
    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }
}