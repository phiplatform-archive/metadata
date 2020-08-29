using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// Represent interface information
    /// </summary>
    internal class InterfaceData : RootMemberData, IInterface
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="type">interface information</param>
        /// <param name="memberConfiguration">member configuration</param>
        /// <param name="memberSeq">interface members</param>
        public InterfaceData(Type type, MemberConfiguration memberConfiguration, IEnumerable<IMemberCore> memberSeq)
            : base(type, memberConfiguration, memberSeq)
        {
            MemberGroup = MemberGroups.Interface;
        }
    }
}
