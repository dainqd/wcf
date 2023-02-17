using System.ComponentModel.DataAnnotations;

namespace baseNetApi.models.Auth;

public class AuthenticateRequest
{
    [Required]
    public string username { get; set; }

    [Required]
    public string password { get; set; }
}