using NoRealm.Phi.Metadata.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NoRealm.Phi.Metadata.Manager
{
    /// <summary>
    /// the metadata manager
    /// </summary>
    public sealed class DefaultMetadataManager : IMetadataManager
    {
        private readonly IServiceProvider serviceProvider;

        private readonly IDictionary<Type, IRootMember> typeCache = new Dictionary<Type, IRootMember>();
        private readonly IDictionary<ExcludeGroup, string[]> excludeGroups = new Dictionary<ExcludeGroup, string[]>();
        private readonly List<Type> excludeTypes;
        private readonly IRootFeatureDetails[] rootMemberFeatures;
        private readonly IMemberFeatureDetails[] memberFeatures;

        private readonly IRootMemberFactory rootMemberFactory;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="serviceProvider">service provider</param>
        /// <param name="metadataContent">builder content</param>
        /// <param name="rootMemberFactory">root member factory</param>
        public DefaultMetadataManager(IServiceProvider serviceProvider, IMetadataContent metadataContent, IRootMemberFactory rootMemberFactory)
        {
            this.serviceProvider = serviceProvider;
            this.rootMemberFactory = rootMemberFactory ?? throw new ArgumentNullException(nameof(rootMemberFactory));

            if (metadataContent == null)
                throw new ArgumentNullException(nameof(metadataContent));

            foreach (var id in Enum.GetValues(typeof(ExcludeGroup)))
                excludeGroups.Add((ExcludeGroup) id, metadataContent.GetExcludedGroup((ExcludeGroup) id).ToArray());

            excludeTypes = new List<Type>(metadataContent.GetExcludedTypes());

            var features = metadataContent.GetFeatures().ToArray();

            rootMemberFeatures = features.OfType<IRootFeatureDetails>().ToArray();
            memberFeatures = features.OfType<IMemberFeatureDetails>().ToArray();

            foreach (var (targetType, deepScan) in metadataContent.GetTypesToCache())
                PrepareType(targetType, deepScan);
        }

        /// <inheritdoc />
        public IReadOnlyList<string> GetExcluded(ExcludeGroup excludeGroup) => excludeGroups[excludeGroup];

        /// <inheritdoc />
        public IReadOnlyList<Type> ExcludedTypes => excludeTypes;

        /// <inheritdoc />
        public bool IsExcluded(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (excludeTypes.Contains(type))
                return true;

            var excludeGroupName = new Dictionary<ExcludeGroup, Func<Type, string>>
            {
                [ExcludeGroup.Assembly] = t => t.Assembly.GetName().Name,
                [ExcludeGroup.Module] = t => t.Module.Name,
                [ExcludeGroup.Namespace] = t => t.Namespace
            };

            foreach (var (group, names) in excludeGroups)
            {
                var inputName = excludeGroupName[group](type).ToLower();

                if (Array.IndexOf(names, inputName) >= 0)
                    return true;

                foreach (var name in names)
                {
                    if (name.EndsWith("*") && Regex.IsMatch(inputName, name))
                        return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public void AddTypes(bool deepScan, params Type[] types)
        {
            foreach (var type in types)
                PrepareType(type, deepScan);
        }

        /// <inheritdoc />
        public IRootMember GetMetadata(Type type)
            => typeCache.TryGetValue(type, out var root) ? root : null;

        /// <summary>
        /// create type metadata
        /// </summary>
        /// <param name="type"></param>
        /// <param name="deepScan"></param>
        private void PrepareType(Type type, bool deepScan)
        {
            if (typeCache.ContainsKey(type)) return;
            if (IsExcluded(type)) return;

            var rootMember = rootMemberFactory.CreateRootMember(type);
            if (rootMember != null) AddFeatures(rootMember);
            typeCache.Add(type, rootMember);

            if (deepScan && rootMember is IMemberCollection memberCollection)
            {
                foreach (var member in memberCollection.Members)
                {
                    type = member.DotNetMember as Type;
                    if (type != null)
                    {
                        if (typeCache.ContainsKey(type)) continue;

                        rootMember = rootMemberFactory.CreateRootMember(type);
                        if (rootMember != null) AddFeatures(rootMember);
                        typeCache.Add(type, rootMember);
                    }
                }
            }
        }

        /// <summary>
        /// add features to root member and child members
        /// </summary>
        /// <param name="rootMember">root member</param>
        private void AddFeatures(IRootMember rootMember)
        {
            foreach (var rootFeature in rootMemberFeatures)
            {
                Func<IServiceProvider, IRootMember, object> factory;

                if (rootFeature.Factory != null)
                    factory = rootFeature.Factory;
                else if (rootFeature.Types.TryGetValue(rootMember.DotNetMember, out factory))
                { /* do nothing */ }

                var feature = factory?.Invoke(serviceProvider, rootMember);
                if (feature == null) continue;
                rootMember.Features[rootFeature.FeatureType] = feature;
            }

            if (!(rootMember is IMemberCollection memberCollection)) return;

            foreach (var memberFeature in memberFeatures)
            {
                foreach (var member in memberCollection.Members)
                {
                    if (!(member is IFeatures content)) return;

                    Func<IServiceProvider, IFeatures, object> factory;

                    if (memberFeature.Factory != null)
                        factory = memberFeature.Factory;
                    else if (memberFeature.Types.TryGetValue(rootMember.DotNetMember, out factory))
                    { /* do nothing */ } 
                    else if (memberFeature.Members.TryGetValue(rootMember.DotNetMember, out var members))
                        members.TryGetValue(member.Name, out factory);

                    var feature = factory?.Invoke(serviceProvider, content);
                    if (feature == null) continue;
                    content.Features[memberFeature.FeatureType] = feature;
                }
            }
        }
    }
}
