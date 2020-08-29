using System;
using System.Collections.Generic;
using System.Linq;

namespace NoRealm.Phi.Metadata.Builder.Internal
{
    internal class RootFeatureDetails : IRootFeatureDetails
    {
        internal Dictionary<Type, Func<IServiceProvider, IRootMember, object>> types;

        /// <inheritdoc />
        public Type FeatureType { get; set; }

        /// <inheritdoc />
        public Func<IServiceProvider, IRootMember, object> Factory { get; set; }
        
        /// <inheritdoc />
        public IReadOnlyDictionary<Type, Func<IServiceProvider, IRootMember, object>> Types => types;
    }

    internal class MemberFeatureDetails : IMemberFeatureDetails
    {
        internal Dictionary<Type, Func<IServiceProvider, IFeatures, object>> types;

        internal Dictionary<Type, Dictionary<string, Func<IServiceProvider, IFeatures, object>>> members;

        /// <inheritdoc />
        public Type FeatureType { get; set; }
        
        /// <inheritdoc />
        public Func<IServiceProvider, IFeatures, object> Factory { get; set; }
        
        /// <inheritdoc />
        public IReadOnlyDictionary<Type, Func<IServiceProvider, IFeatures, object>> Types => types;
        
        /// <inheritdoc />
        public IReadOnlyDictionary<Type, IReadOnlyDictionary<string, Func<IServiceProvider, IFeatures, object>>> Members
            => members.ToDictionary(e => e.Key,
                e => (IReadOnlyDictionary<string, Func<IServiceProvider, IFeatures, object>>) e.Value);
    }

    /// <summary>
    /// feature extensions
    /// </summary>
    internal static class FeatureExtensions
    {
        /// <summary>
        /// add items to dictionary
        /// </summary>
        /// <typeparam name="T">type name</typeparam>
        /// <param name="content">dictionary to modify</param>
        /// <param name="types">key items to add into dictionary</param>
        /// <param name="factory">value item</param>
        public static void AddRange<T>(this IDictionary<Type, Func<IServiceProvider, T, object>> content,
            IEnumerable<Type> types,
            Func<IServiceProvider, T, object> factory)
            where T : IFeatures
        {
            foreach (var type in types)
            {
                if (content.ContainsKey(type))
                    content[type] = factory;
                else
                    content.Add(type, factory);
            }
        }
    }
}
