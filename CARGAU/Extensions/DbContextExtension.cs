using Domain.Interfaces.DbContext;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CARGAU.Extensions;

public static class DbContextExtension
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TuningStudioDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(TuningStudioDbContext)));
            }
        );

        services.AddScoped<ITuningStudioDbContext, TuningStudioDbContext>();

        return services;
    }
}
