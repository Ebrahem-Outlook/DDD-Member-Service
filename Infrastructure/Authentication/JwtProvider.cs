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
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
                new Claim(ClaimTypes.Name, member.FirstName),
                new Claim(ClaimTypes.Email, member.Email)
            };

            DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                null,
                expires: tokenExpirationTime,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);    
        }
        catch(Exception ex)
        {
            // 
            throw new Exception("An error occurred while generating the JWT token.", ex);
        }
    }
}
