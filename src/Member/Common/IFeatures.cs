using NoRealm.Phi.Shared.Features;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// allow a member to have a list of features
    /// </summary>
    public interface IFeatures
    {
        /// <summary>
        /// get associated features
        /// </summary>
        IFeatureCollection Features { get; }
    }
}
