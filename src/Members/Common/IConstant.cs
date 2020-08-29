using NoRealm.Phi.Metadata.Extensions;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Represent a constant value
    /// </summary>
    public interface IConstant : IMember, IReadableMember
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
        /// Get constant value.
        /// </summary>
        object Value { get; }
    }
}
