using System;
using NoRealm.Phi.Metadata.Activator;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Provide methods for registering the activator and its extensions
    /// </summary>
    public static class ActivatorDependencyInjection
    {
        /// <summary>
        ///     Register an activator.
        /// </summary>
        /// <typeparam name="T">the activator type.</typeparam>
        /// <param name="services">the service collection.</param>
        /// <param name="serviceLifetime">the service life time.</param>
        /// <returns>A reference to this service collection instance after the operation has completed.</returns>
        public static IServiceCollection RegisterActivator<T>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where T : class, IActivator
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton: return services.AddSingleton<IActivator, T>();
                case ServiceLifetime.Scoped: return services.AddScoped<IActivator, T>();
                case ServiceLifetime.Transient: return services.AddTransient<IActivator, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Add a Constructor provider.
        /// </summary>
        /// <typeparam name="T">the provider type</typeparam>
        /// <param name="services">the service collection.</param>
        /// <returns>A reference to this service collection instance after the operation has completed.</returns>
        public static IServiceCollection AddConstructorProvider<T>(this IServiceCollection services)
            where T: class, IConstructorProvider
        {
            return services.AddSingleton<IConstructorProvider, T>();
        }

        /// <summary>
        ///     Register default activator type.
        /// </summary>
        /// <param name="services">the service collection.</param>
        /// <returns>A reference to this service collection instance after the operation has completed.</returns>
        public static IServiceCollection RegisterDefaultActivator(this IServiceCollection services)
        {
            return services.RegisterActivator<DefaultActivator>();
        }

        /// <summary>
        ///     Add provider which fetch all public constructors from a type.
        /// </summary>
        /// <param name="services">the service collection.</param>
        /// <returns>A reference to this service collection instance after the operation has completed.</returns>
        public static IServiceCollection AddDefaultConstructorProvider(this IServiceCollection services)
        {
            return services.AddConstructorProvider<PublicConstructorProvider>();
        }
    }
}
