using NoRealm.Phi.Metadata.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     Provide methods for registering metadata builder
    /// </summary>
    public static class MetadataBuilderDependencyInjection
    {
        private static readonly DefaultMetadataBuilder MetadataBuilder = new DefaultMetadataBuilder();

        /// <summary>
        ///     Create <see cref="IMetadataBuilder"/> and register its <see cref="IMetadataContent"/>
        /// </summary>
        /// <param name="services">The service collection to contain the registration.</param>
        /// <returns>A reference to metadata builder.</returns>
        public static IMetadataBuilder RegisterDefaultMetadataBuilder(this IServiceCollection services)
        {
            services
                .AddSingleton(e => MetadataBuilder.GetContent());

            return MetadataBuilder;
        }
    }
}
