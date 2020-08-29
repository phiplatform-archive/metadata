using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Builder.Internal
{
    /// <summary>
    /// this class used instead of <seealso cref="MetadataBuilder"/> after the content is initialized
    /// </summary>
    internal class ProtectedMetadataBuilder : IMetadataBuilder
    {
        private readonly NotSupportedException exception = 
            new NotSupportedException("you can not modify builder after content is initialized.");

        /// <inheritdoc />
        public IMetadataBuilder Exclude(ExcludeGroup excludeGroup, string excludeName) => throw exception;

        /// <inheritdoc />
        public IMetadataBuilder Exclude(params Type[] types) => throw exception;

        /// <inheritdoc />
        public IMetadataBuilder AddTypes(bool deepScan, params Type[] types) => throw exception;

        /// <inheritdoc />
        public IMetadataBuilder AddRootFeature(Type featureType,
            Func<IServiceProvider, IRootMember, object> featureFactory, params Type[] types)
            => throw exception;

        /// <inheritdoc />
        public IMetadataBuilder AddMemberFeature(Type featureType, Func<IServiceProvider, IFeatures, object> featureFactory,
            params (Type Type, IEnumerable<string> Members)[] memberNames)
            => throw exception;
    }
}
