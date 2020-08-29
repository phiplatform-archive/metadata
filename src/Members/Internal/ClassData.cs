using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// Represent class information
    /// </summary>
    internal class ClassData : RootMemberData, IClass
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="type">class information</param>
        /// <param name="memberConfiguration">member configuration</param>
        /// <param name="memberSeq">class members</param>
        public ClassData(Type type, MemberConfiguration memberConfiguration, IEnumerable<IMemberCore> memberSeq)
            : base(type, memberConfiguration, memberSeq)
        {
            MemberGroup = MemberGroups.Class;
        }
    }
}
