using System;
using System.Collections.Generic;
using System.Linq;

namespace NoRealm.Phi.Metadata.Builder.Internal
{
    /// <summary>
    /// represent content of a <see cref="MetadataBuilder"/>
    /// </summary>
    internal class MetadataContent : IMetadataContent
    {
        private readonly MetadataBuilder builder;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="builder">metadata builder instance</param>
        internal MetadataContent(MetadataBuilder builder)
        {
            this.builder = builder;
        }

        /// <inheritdoc />
        public IEnumerable<string> GetExcludedGroup(ExcludeGroup excludeGroup)
            => builder.excludeGroups.TryGetValue(excludeGroup, out var set)? set: Enumerable.Empty<string>();

        /// <inheritdoc />
        public IEnumerable<Type> GetExcludedTypes()
            => builder.excludeTypes;

        /// <inheritdoc />
        public IEnumerable<(Type TargetType, bool DeepScan)> GetTypesToCache()
            => builder.typesCache.Values;

        /// <inheritdoc />
        public IEnumerable<IFeatureDetails> GetFeatures()
        {
            return builder.rootFeatures.Select(e => (IFeatureDetails) e.Value)
                .Concat(builder.memberFeatures.Select(e => e.Value));
        }
    }
}
