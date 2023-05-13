namespace api.Models.DTO;

public class LoginResponseDto
{
    public string userId { get; set; }
    public string refreshToken { get; set; }
    public DateTime expiration { get; set; }
}