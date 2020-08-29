using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// a factory for creating a members which represent a specific <see cref="MemberInfo"/>
    /// </summary>
    public interface IMemberFactory
    {
        /// <summary>
        /// create member instance
        /// </summary>
        /// <param name="memberInfo">member information</param>
        /// <returns><see cref="IMemberCore"/> instance for input member.</returns>
        /// <exception cref="NotSupportedException">if input member is not supported.</exception>
        IMemberCore CreateMember(MemberInfo memberInfo);
    }
}
