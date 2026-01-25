using AssetStore.Application;
using AssetStore.DataAccess.Postgres;
using Microsoft.AspNetCore.Mvc;

namespace AssetStore.API;

/// <summary>
/// Provides extension methods for configuring and setting up dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <param name="services">
    ///     The <see cref="IServiceCollection"/> to add services to.
    /// </param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers dependency injection services across all application layers used by this program.
        /// </summary>
        /// <param name="configuration"><see cref="ConfigurationManager"/> for configuration settings.</param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.
        /// </returns>
        /// <remarks>
        /// This method is intended to be called during startup (e.g., in <c>Program.cs</c>) to keep DI registration organized.
        /// </remarks>
        public IServiceCollection AddProgramDependencies(ConfigurationManager configuration)
        {
            return services
                .AddWebDependencies()
                .AddPostgresInfrastructure(configuration)
                .AddApplicationDependencies();
        }

        private IServiceCollection AddWebDependencies()
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.ReturnHttpNotAcceptable = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}