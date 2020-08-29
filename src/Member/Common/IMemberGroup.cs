using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// represent a group of members
    /// </summary>
    public interface IMemberGroup
    {
        /// <summary>
        /// get group id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// get member type
        /// </summary>
        MemberTypes MemberType { get; }
    }
}
