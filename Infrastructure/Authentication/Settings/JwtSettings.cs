namespace Infrastructure.Authentication.Settings;

internal class JwtSettings
{

    public const string SettingsKey = "Jwt";

    /// <summary>
    /// Initialze a new instance of the <see cref="JwtSettings"/> calss.
    /// </summary>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="securityKey"></param>
    /// <param name="tokenExpirationInMinutes"></param>
    public JwtSettings()
    {
        Issuer = string.Empty;
        Audience = string.Empty;
        SecurityKey = string.Empty;
    }


    /// <summary>
    /// Gets or Sets the Issuer.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Gets or Set the audience.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Gets or sets the security key.
    /// </summary>
    public string SecurityKey { get; set; }

    /// <summary>
    /// Gets or sets the token expiration time in minutes.
    /// </summary>
    public int TokenExpirationInMinutes { get; set; }
}
