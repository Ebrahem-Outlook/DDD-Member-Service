using Application.Core.Abstractions.Data;
using Domain.Members;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connection = configuration.GetConnectionString("Local-SqlServer");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

        services.AddScoped<IDbContext>(options => options.GetRequiredService<AppDbContext>());

        services.AddScoped<IUnitOfWork>(options => options.GetRequiredService<AppDbContext>());

        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }
}
