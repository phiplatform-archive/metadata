using System;

namespace NoRealm.Phi.Metadata.Extensions
{
    /// <summary>
    /// Represent a member with read access
    /// </summary>
    public interface IReadableMember
    {
        /// <summary>
        /// Get type of content to read.
        /// </summary>
        Type ReadContentType { get; }

        /// <summary>
        /// Get member value.
        /// </summary>
        /// <param name="context">read context.</param>
        /// <returns>value in input instance.</returns>
        object GetValue(IReadContext context);
    }
}
