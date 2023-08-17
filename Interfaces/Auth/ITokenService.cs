using gantt_backend.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace  gantt_backend.Interfaces.Auth;

public interface ITokenService
{
    JwtSecurityToken GetToken(List<Claim> authClaims);
}