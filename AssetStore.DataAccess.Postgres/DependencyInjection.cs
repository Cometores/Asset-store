using AssetStore.Core.Abstractions;
using AssetStore.DataAccess.Postgres.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetStore.DataAccess.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AssetStoreDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AssetStoreDB"));
        });

        services.AddScoped<IAssetsRepository, AssetsRepository>();

        return services;
    }
}