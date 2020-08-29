using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Builder
{
    /// <summary>
    /// Represent feature builder for <seealso cref="IRootMember"/>
    /// </summary>
    public interface IRootFeatureDetails : IFeatureDetails
    {
        /// <summary>
        /// Get feature global factory
        /// </summary>
        Func<IServiceProvider, IRootMember, object> Factory { get; }

        /// <summary>
        /// Get feature map between type and factory
        /// </summary>
        IReadOnlyDictionary<Type, Func<IServiceProvider, IRootMember, object>> Types { get; }
    }
}
