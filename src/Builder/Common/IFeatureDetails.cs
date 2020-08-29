using System;

namespace NoRealm.Phi.Metadata.Builder
{
    /// <summary>
    /// Represent feature information
    /// </summary>
    public interface IFeatureDetails
    {
        /// <summary>
        /// Get feature type
        /// </summary>
        Type FeatureType { get; }
    }
}
