using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController (JwtTokenHandler jwtTokenHandler) : ControllerBase
{
    private readonly JwtTokenHandler _jwtTokenHandler = jwtTokenHandler;

    [HttpPost("authenticate")]
    public ActionResult<AuthenticationResponseModel?> Authenticate ([FromBody] AuthenticationRequestModel request)
    {
        var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(request);

        if (authenticationResponse == null)
        {
            return Unauthorized();
        }

        return authenticationResponse;
    }
}
