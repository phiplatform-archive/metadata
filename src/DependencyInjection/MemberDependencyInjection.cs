using System;
using NoRealm.Phi.Metadata;
using NoRealm.Phi.Metadata.Members;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Provide methods for registering member extensions
    /// </summary>
    public static class MemberDependencyInjection
    {
        /// <summary>
        ///     Register member group provider
        /// </summary>
        /// <typeparam name="TProvider">member group provider type.</typeparam>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <param name="serviceLifetime">the service life time.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterMemberGroupProvider<TProvider>(
            this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TProvider : class, IMemberGroupProvider
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton: return services.AddSingleton<IMemberGroupProvider, TProvider>();
                case ServiceLifetime.Scoped: return services.AddScoped<IMemberGroupProvider, TProvider>();
                case ServiceLifetime.Transient: return services.AddTransient<IMemberGroupProvider, TProvider>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Register default member group provider
        /// </summary>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterDefaultMemberGroupProvider(this IServiceCollection services)
        {
            return services.RegisterMemberGroupProvider<DefaultMemberGroupProvider>();
        }

        /// <summary>
        ///     Register members provider
        /// </summary>
        /// <typeparam name="TProvider">members provider type.</typeparam>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <param name="serviceLifetime">the service life time.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterMembersProvider<TProvider>(
            this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TProvider : class, IMembersProvider
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton: return services.AddSingleton<IMembersProvider, TProvider>();
                case ServiceLifetime.Scoped: return services.AddScoped<IMembersProvider, TProvider>();
                case ServiceLifetime.Transient: return services.AddTransient<IMembersProvider, TProvider>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Register default members provider
        /// </summary>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterDefaultMembersProvider(this IServiceCollection services)
        {
            return services.RegisterMembersProvider<DefaultMembersProvider>();
        }

        /// <summary>
        ///     Register member factory
        /// </summary>
        /// <typeparam name="TFactory">member factory type.</typeparam>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <param name="serviceLifetime">the service life time.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterMemberFactory<TFactory>(
            this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TFactory : class, IMemberFactory
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton: return services.AddSingleton<IMemberFactory, TFactory>();
                case ServiceLifetime.Scoped: return services.AddScoped<IMemberFactory, TFactory>();
                case ServiceLifetime.Transient: return services.AddTransient<IMemberFactory, TFactory>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Register default member factory
        /// </summary>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterDefaultMemberFactory(this IServiceCollection services)
        {
            return services.RegisterMemberFactory<DefaultMemberFactory>();
        }

        /// <summary>
        ///     Register root member factory
        /// </summary>
        /// <typeparam name="TFactory">root member factory type.</typeparam>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <param name="serviceLifetime">the service life time.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterRootMemberFactory<TFactory>(
            this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
            where TFactory : class, IRootMemberFactory
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton: return services.AddSingleton<IRootMemberFactory, TFactory>();
                case ServiceLifetime.Scoped: return services.AddScoped<IRootMemberFactory, TFactory>();
                case ServiceLifetime.Transient: return services.AddTransient<IRootMemberFactory, TFactory>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Register default root member factory
        /// </summary>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterDefaultRootMemberFactory(this IServiceCollection services)
        {
            return services.RegisterRootMemberFactory<DefaultRootMemberFactory>();
        }

        /// <summary>
        ///     Register member configuration
        /// </summary>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <param name="memberConfiguration"></param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterMemberConfiguration(
            this IServiceCollection services, MemberConfiguration memberConfiguration)
        {
            if (memberConfiguration == null)
                throw new ArgumentNullException(nameof(memberConfiguration));

            return services.AddSingleton(memberConfiguration);
        }
    }
}
