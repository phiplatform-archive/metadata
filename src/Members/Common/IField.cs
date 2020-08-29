using NoRealm.Phi.Metadata.Extensions;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Represent a field metadata
    /// </summary>
    public interface IField : IMember, IReadableMember, IWritableMember
    {
        /// <summary>
        /// Get .net <see cref="FieldInfo"/> instance
        /// </summary>
        new FieldInfo DotNetMember { get; }

        /// <summary>
        /// Get field characteristics.
        /// </summary>
        FieldAttributes Flags { get; }

        /// <summary>
        /// Get whether field is readonly
        /// </summary>
        bool IsReadOnly { get; }
    }
}
