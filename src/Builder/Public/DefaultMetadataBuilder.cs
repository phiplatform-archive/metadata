using NoRealm.Phi.Metadata.Builder.Internal;
using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Builder
{
    /// <summary>
    /// Default metadata builder
    /// </summary>
    public sealed class DefaultMetadataBuilder : IMetadataBuilder
    {
        private IMetadataBuilder builder = new MetadataBuilder();
        private IMetadataContent content;

        /// <inheritdoc />
        public IMetadataBuilder Exclude(ExcludeGroup excludeGroup, string excludeName)
            => builder.Exclude(excludeGroup, excludeName);

        /// <inheritdoc />
        public IMetadataBuilder Exclude(params Type[] types)
            => builder.Exclude(types);

        /// <inheritdoc />
        public IMetadataBuilder AddTypes(bool deepScan, params Type[] types)
            => builder.AddTypes(deepScan, types);

        /// <inheritdoc />
        public IMetadataBuilder AddRootFeature(Type featureType,
            Func<IServiceProvider, IRootMember, object> featureFactory, params Type[] types)
            => builder.AddRootFeature(featureType, featureFactory, types);

        /// <inheritdoc />
        public IMetadataBuilder AddMemberFeature(Type featureType,
            Func<IServiceProvider, IFeatures, object> featureFactory,
            params (Type Type, IEnumerable<string> Members)[] memberNames)
            => builder.AddMemberFeature(featureType, featureFactory, memberNames);

        /// <summary>
        /// Get the content of the builder and prevent future modifications
        /// </summary>
        /// <returns>an instance of <see cref="IMetadataBuilder"/> which represent this builder</returns>
        public IMetadataContent GetContent()
        {
            if (content != null)
                return content;

            content = new MetadataContent((MetadataBuilder)builder);
            builder = new ProtectedMetadataBuilder();

            return content;
        }
    }
}
