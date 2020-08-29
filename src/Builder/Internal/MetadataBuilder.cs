using System;
using System.Collections.Generic;
using System.Linq;

namespace NoRealm.Phi.Metadata.Builder.Internal
{
    /// <summary>
    /// A metadata builder
    /// </summary>
    internal class MetadataBuilder : IMetadataBuilder
    {
        internal readonly Dictionary<ExcludeGroup, HashSet<string>> excludeGroups = new Dictionary<ExcludeGroup, HashSet<string>>();
        internal readonly HashSet<Type> excludeTypes = new HashSet<Type>();

        internal readonly Dictionary<Type, (Type TargetType, bool DeepScan)> typesCache =
            new Dictionary<Type, (Type, bool)>();

        internal readonly Dictionary<Type, RootFeatureDetails> rootFeatures =
            new Dictionary<Type, RootFeatureDetails>();

        internal readonly Dictionary<Type, MemberFeatureDetails> memberFeatures =
            new Dictionary<Type, MemberFeatureDetails>();

        /// <inheritdoc />
        public IMetadataBuilder Exclude(ExcludeGroup excludeGroup, string excludeName)
        {
            if (string.IsNullOrWhiteSpace(excludeName))
                throw new ArgumentException(
                    "a name should not be null or contains whitespaces", nameof(excludeName));

            if (!excludeGroups.TryGetValue(excludeGroup, out var set))
            {
                set = new HashSet<string>();
                excludeGroups.Add(excludeGroup, set);
            }

            set.Add(excludeName.ToLower());
            return this;
        }

        /// <inheritdoc />
        public IMetadataBuilder Exclude(params Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var type in types)
                excludeTypes.Add(type);

            return this;
        }

        /// <inheritdoc />
        public IMetadataBuilder AddTypes(bool deepScan, params Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            foreach (var type in types)
            {
                if (type == null) continue;

                if (typesCache.ContainsKey(type))
                    typesCache[type] = (type, deepScan);
                else
                    typesCache.Add(type, (type, deepScan));
            }

            return this;
        }

        /// <inheritdoc />
        public IMetadataBuilder AddRootFeature(Type featureType, Func<IServiceProvider, IRootMember, object> featureFactory, params Type[] types)
        {
            if (featureType == null)
                throw new ArgumentNullException(nameof(featureType));

            if (featureFactory == null)
                throw new ArgumentNullException(nameof(featureFactory));

            if (types == null)
                throw new ArgumentNullException(nameof(types));

            if (rootFeatures.TryGetValue(featureType, out var content))
            {
                if (content.Factory == null)
                {
                    if (types.Length == 0)
                    {
                        content.Factory = featureFactory;
                        content.types = null;
                    }
                    else
                    {
                        content.Factory = null;
                        content.types = new Dictionary<Type, Func<IServiceProvider, IRootMember, object>>();
                        content.types.AddRange(types, featureFactory);
                    }
                }
            }
            else
            {
                content = new RootFeatureDetails
                {
                    FeatureType = featureType,
                    types = new Dictionary<Type, Func<IServiceProvider, IRootMember, object>>()
                };

                if (types.Length == 0)
                    content.Factory = featureFactory;
                else
                    content.types.AddRange(types, featureFactory);

                rootFeatures.Add(featureType, content);
            }

            return this;
        }

        /// <inheritdoc />
        public IMetadataBuilder AddMemberFeature(Type featureType, Func<IServiceProvider, IFeatures, object> featureFactory,
            params (Type Type, IEnumerable<string> Members)[] memberNames)
        {
            if (featureType == null)
                throw new ArgumentNullException(nameof(featureType));

            if (featureFactory == null)
                throw new ArgumentNullException(nameof(featureFactory));

            if (memberNames == null)
                throw new ArgumentNullException(nameof(memberNames));

            if (memberNames.Any(member => member.Type == null))
                throw new ArgumentException("value of tuple member 'Type' can not be null.", nameof(memberNames));

            if (!memberFeatures.TryGetValue(featureType, out var content))
            {
                if (memberNames.Length == 0)
                {
                    memberFeatures.Add(featureType, new MemberFeatureDetails
                    {
                        FeatureType = featureType,
                        Factory = featureFactory
                    });
                    return this;
                }
                content = new MemberFeatureDetails{FeatureType = featureType};
            }

            if (content.Factory != null) return this;

            content.types ??= new Dictionary<Type, Func<IServiceProvider, IFeatures, object>>();
            content.members ??= new Dictionary<Type, Dictionary<string, Func<IServiceProvider, IFeatures, object>>>();

            foreach (var (type, members) in memberNames)
            {
                if (content.types.ContainsKey(type))
                {
                    content.types[type] = featureFactory;
                    continue;
                }

                var values = members?.ToList();

                if (values == null || values.Count == 0)
                {
                    if (content.members.ContainsKey(type))
                        content.members.Remove(type);

                    content.types.Add(type, featureFactory);
                    continue;
                }

                if (!content.members.TryGetValue(type, out var data))
                {
                    data = new Dictionary<string, Func<IServiceProvider, IFeatures, object>>();
                    content.members.Add(type, data);
                }

                foreach (var value in values)
                {
                    if (data.ContainsKey(value))
                        data.Add(value, featureFactory);
                }
            }

            return this;
        }
    }
}
