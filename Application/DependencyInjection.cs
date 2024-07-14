using Application.Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

            cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));

            cfg.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
        });

        return services;
    }
}
