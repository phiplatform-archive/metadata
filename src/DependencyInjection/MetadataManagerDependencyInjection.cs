using NoRealm.Phi.Metadata.Manager;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Provide methods for registering the metadata manager
    /// </summary>
    public static class MetadataManagerDependencyInjection
    {
        /// <summary>
        ///     Register a metadata manager.
        /// </summary>
        /// <typeparam name="T">metadata manager type.</typeparam>
        /// <param name="services">the service collection.</param>
        /// <param name="serviceLifetime">the service life time.</param>
        /// <returns>A reference to this service collection instance after the operation has completed.</returns>
        public static IServiceCollection RegisterMetadataManager<T>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where T : class, IMetadataManager
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton: return services.AddSingleton<IMetadataManager, T>();
                case ServiceLifetime.Scoped: return services.AddScoped<IMetadataManager, T>();
                case ServiceLifetime.Transient: return services.AddTransient<IMetadataManager, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Register the default metadata manager type
        /// </summary>
        /// <param name="services">the service collection.</param>
        /// <returns>A reference to this service collection instance after the operation has completed.</returns>
        public static IServiceCollection RegisterDefaultMetadataManager(this IServiceCollection services)
        {
            return services.RegisterMetadataManager<DefaultMetadataManager>();
        }
    }
}
