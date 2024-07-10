using Application.Core.Abstractins.Authentication;
using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Data;
using Domain.Members;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Settings;
using Infrastructure.Caching;
using Infrastructure.Common;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connection = configuration.GetConnectionString("Local-SqlServer");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());


        services.AddMemoryCache();

        services.AddScoped<MemberRepository>();

        services.AddScoped<IMemberRepository>(serviceProvider =>
        {
            var decorated = serviceProvider.GetRequiredService<MemberRepository>();
            var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();

            return new CachedMemberRepository(decorated, memoryCache);
        });


        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]))
                   });

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IDateTime, MachineDateTime>();

        return services;
    }
}
