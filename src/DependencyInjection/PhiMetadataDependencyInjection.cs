using NoRealm.Phi.Metadata;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Provide methods for registering default services for Phi metadata
    /// </summary>
    public static class PhiMetadataDependencyInjection
    {
        /// <summary>
        ///     Register all default metadata library default services
        /// </summary>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <param name="memberConfiguration"></param>
        /// <returns>A reference to metadata builder.</returns>
        public static IServiceCollection RegisterMetadataDefaultServices(
            this IServiceCollection services, MemberConfiguration memberConfiguration)
        {
            return services
                .RegisterMemberConfiguration(memberConfiguration)
                .RegisterDefaultMemberGroupProvider()
                .RegisterDefaultMembersProvider()
                .RegisterDefaultMemberFactory()
                .RegisterDefaultRootMemberFactory()
                .RegisterDefaultMetadataManager()
                .RegisterDefaultActivator()
                .AddDefaultConstructorProvider();
        }
    }
}
