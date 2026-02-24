using AssetStore.Application.Services;
using AssetStore.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AssetStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        return services.AddScoped<IAssetsService, AssetsService>();
    }
}