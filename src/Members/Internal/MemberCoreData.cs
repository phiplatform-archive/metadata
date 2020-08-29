using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent member core information
    /// </summary>
    internal abstract class MemberCoreData : IMemberCore
    {
        /// <inheritdoc />
        public object DotNetMember { get;  protected set; }

        /// <inheritdoc />
        public IMemberCore DeclaringMember { get; protected internal set; }

        /// <inheritdoc />
        public IReadOnlyList<Attribute> Attributes { get; protected set; }

        public IMemberGroup MemberGroup { get; protected set; } = MemberGroups.None;

        /// <inheritdoc />
        public string Name { get; protected set; }
        
        /// <inheritdoc />
        public ulong HashCode { get; protected set; }
    }
}
