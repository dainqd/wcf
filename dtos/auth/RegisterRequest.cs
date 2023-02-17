using System.ComponentModel.DataAnnotations;

namespace baseNetApi.models.Auth;

public class RegisterRequest
{
    [Required]
    public string username { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    [Compare("password")]
    public string confirmPassword { get; set; }
}