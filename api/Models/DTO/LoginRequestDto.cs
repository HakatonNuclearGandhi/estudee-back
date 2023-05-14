using System.ComponentModel.DataAnnotations;

namespace api.Models.DTO;

public class LoginRequestDto
{
    [Required(ErrorMessage = "User Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}