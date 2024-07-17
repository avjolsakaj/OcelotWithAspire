using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace JwtAuthenticationManager;

public class JwtTokenHandler
{
    public const string JWT_SECURITY_KEY = "oidjf23p98umc3po8ucm#$%^&*.somveryinterestingkey";
    private const int JWT_TOKEN_VALIDITY_MINS = 20;
    private readonly List<UserAccount> _userAccountList;

    public JwtTokenHandler ()
    {
        _userAccountList =
        [
            new UserAccount {UserName = "admin", Password = "admin123", Role = "Admin"},
            new UserAccount {UserName = "user", Password = "user123", Role = "User"}
        ];

    }

    public AuthenticationResponseModel? GenerateJwtToken (AuthenticationRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
        {
            return null;
        }

        var userAccount = _userAccountList.Find(x => x.UserName == request.UserName && x.Password == request.Password);
        if (userAccount == null)
        {
            return null;
        }

        var tokenExiperyTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

        var claimsIdentity = new ClaimsIdentity(
        [
            new Claim(JwtRegisteredClaimNames.Name, request.UserName),
            new Claim("Role", userAccount.Role)
        ]);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExiperyTimeStamp,
            SigningCredentials = signingCredentials
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        return new AuthenticationResponseModel
        {
            UserName = userAccount.UserName,
            ExpiresIn = (int) tokenExiperyTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            JwtToken = token
        };

    }

}
