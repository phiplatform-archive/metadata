using NoRealm.Phi.Metadata.Extensions;

namespace NoRealm.Phi.Metadata.Internal
{
    /// <summary>
    /// a context for reading values
    /// </summary>
    internal sealed class ReadContext : IReadContext
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="instance">object instance; null if member is static</param>
        internal ReadContext(object instance)
            => Instance = instance;

        /// <inheritdoc />
        public object Instance { get; }
    }
}
