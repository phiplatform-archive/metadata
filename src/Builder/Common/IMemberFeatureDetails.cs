using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Builder
{
    /// <summary>
    /// Represent a feature builder which get added on any member implements <seealso cref="IFeatures"/>
    /// </summary>
    public interface IMemberFeatureDetails : IFeatureDetails
    {
        /// <summary>
        /// Get feature factory to add on all members of a root member
        /// </summary>
        Func<IServiceProvider, IFeatures, object> Factory { get; }

        /// <summary>
        /// Get feature factory for members of specific type
        /// </summary>
        IReadOnlyDictionary<Type, Func<IServiceProvider, IFeatures, object>> Types { get; }

        /// <summary>
        /// Get feature factory for specific members of specific type
        /// </summary>
        IReadOnlyDictionary<Type, IReadOnlyDictionary<string, Func<IServiceProvider, IFeatures, object>>> Members { get; }
    }
}
