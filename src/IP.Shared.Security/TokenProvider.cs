using IP.Shared.Abstractions.Auths;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace IP.Shared.Security;

public interface ITokenProvider
{
    string Create(IAppUserTokenData user);
}

internal sealed class TokenProvider(IOptions<AuthConfigurationOptions> authOptions) : ITokenProvider
{
    private readonly AuthConfigurationOptions authConfig = authOptions.Value;

    public string Create(IAppUserTokenData user)
    {
        string secretKey = authConfig.SecretKey;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Profile, user.Profile)
        ];

        foreach (string claim in user.Claims)
            claims.Add(new Claim(JwtCustomClaimNames.Permission, claim));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(authConfig.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = authConfig.Issuer,
            Audience = authConfig.Audience
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}