using System;

namespace NoRealm.Phi.Metadata.Extensions
{
    /// <summary>
    /// Represent a member with write access
    /// </summary>
    public interface IWritableMember
    {
        /// <summary>
        /// Get type of content to write.
        /// </summary>
        Type WriteContentType { get; }

        /// <summary>
        /// Set member value.
        /// </summary>
        /// <param name="context">write instance.</param>
        void SetValue(IWriteContext context);
    }
}
