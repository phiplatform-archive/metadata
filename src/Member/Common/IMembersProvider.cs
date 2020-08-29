using System;
using System.Collections.Generic;
using System.Reflection;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// Get members of a input type
    /// </summary>
    public interface IMembersProvider
    {
        /// <summary>
        /// get members of input type
        /// </summary>
        /// <param name="type">type to enumerate its member</param>
        /// <returns>a sequence of members</returns>
        IEnumerable<MemberInfo> GetMembers(Type type);
    }
}
