using System;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// a configuration for a member
    /// </summary>
    public sealed class MemberConfiguration
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="disposeFeatureOnRemove">set to true to dispose a feature upon remove from features collection</param>
        public MemberConfiguration(bool disposeFeatureOnRemove)
        {
            DisposeFeatureOnRemove = disposeFeatureOnRemove;
        }

        /// <summary>
        /// when set to true, make a call to <see cref="IDisposable.Dispose"/> if exists upon removing feature from <see cref="IFeatures.Features"/>
        /// </summary>
        public bool DisposeFeatureOnRemove { get; }
    }
}
