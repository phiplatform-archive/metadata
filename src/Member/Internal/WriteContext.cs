using NoRealm.Phi.Metadata.Extensions;

namespace NoRealm.Phi.Metadata.Internal
{
    /// <summary>
    /// a context for writing values
    /// </summary>
    internal sealed class WriteContext : IWriteContext
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="instance">object instance; null if member is static</param>
        /// <param name="value">a value to write</param>
        internal WriteContext(object instance, object value)
        {
            Instance = instance;
            Value = value;
        }

        /// <inheritdoc />
        public object Instance { get; }

        /// <inheritdoc />
        public object Value { get; }
    }
}
