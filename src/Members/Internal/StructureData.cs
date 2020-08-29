using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// Represent Structure information
    /// </summary>
    internal class StructureData : RootMemberData, IStructure
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="type">structure information</param>
        /// <param name="memberConfiguration">member configuration</param>
        /// <param name="memberSeq">structure members</param>
        public StructureData(Type type, MemberConfiguration memberConfiguration, IEnumerable<IMemberCore> memberSeq)
            : base(type, memberConfiguration, memberSeq)
        {
            MemberGroup = MemberGroups.Structure;
        }
    }
}
