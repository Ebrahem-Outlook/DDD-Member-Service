using Application.Core.Abstractins.Authentication;
using Application.Core.Abstractions.Common;
using Domain.Members;
using Infrastructure.Authentication.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

/// <summary>
/// Represents the JWT provider.
/// </summary>
internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTime _dateTime;
    public JwtProvider(IOptions<JwtSettings> jwtOptions, IDateTime dateTime)
    {
        _jwtSettings = jwtOptions.Value;
        _dateTime = dateTime;
    }

    public string GenerateToken(Member member)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

        var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        Claim[] claims =
        {
            new Claim("MemberId", member.Id.ToString()),
            new Claim("name", $"{member.FirstName} {member.LastName}"),
            new Claim("email", member.Email)
        };

        DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredential);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
