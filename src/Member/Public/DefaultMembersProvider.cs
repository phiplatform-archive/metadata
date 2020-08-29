using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// A provider which enumerate public members of a type
    /// </summary>
    public class DefaultMembersProvider : IMembersProvider
    {
        /// <inheritdoc />
        /// <remarks>all members get enumerated but nested types, events, custom members</remarks>
        public virtual IEnumerable<MemberInfo> GetMembers(Type type)
        {
            var members = new List<MemberInfo>();

            members.AddRange(type.GetProperties());
            members.AddRange(type.GetFields());
            //members.AddRange(type.GetEvents());
            members.AddRange(type.GetConstructors());
            members.AddRange(type.GetMethods(Public | Instance | Static).Where(e => !e.IsSpecialName));

            return members;
        }
    }
}
