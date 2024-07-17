namespace JwtAuthenticationManager.Models;

public class AuthenticationRequestModel
{
    public required string UserName { get; set; }

    public required string Password { get; set; }
}
