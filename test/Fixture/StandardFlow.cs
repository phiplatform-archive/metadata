using System;
using Microsoft.Extensions.DependencyInjection;
using NoRealm.Phi.Metadata.Builder;
using NoRealm.Phi.Metadata.Test.Data;

namespace NoRealm.Phi.Metadata.Test.Fixture
{
    /// <summary>
    /// represent the standard flow 
    /// </summary>
    public class StandardFlow : IRequirement
    {
        private readonly IServiceProvider provider;

        /// <summary>
        /// initialize new instance
        /// </summary>
        public StandardFlow()
        {
            var services = new ServiceCollection();

            services
                .RegisterMetadataDefaultServices(new MemberConfiguration(true));

            var builder = services.RegisterDefaultMetadataBuilder();

            builder.AddType<User>();

            provider = services.BuildServiceProvider();
        }

        /// <inheritdoc/>
        public T GetInstance<T>() where T : class => provider.GetRequiredService<T>();
    }
}
