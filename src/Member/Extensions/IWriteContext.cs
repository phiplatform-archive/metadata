namespace NoRealm.Phi.Metadata.Extensions
{
    /// <summary>
    /// A context for writing data
    /// </summary>
    public interface IWriteContext : IInstanceContext
    {
        /// <summary>
        /// Get data to write
        /// </summary>
        object Value { get; }
    }
}
