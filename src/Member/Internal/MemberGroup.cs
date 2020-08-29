using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Internal
{
    /// <summary>
    /// An implementation of <see cref="IMemberGroup"/>
    /// </summary>
    internal sealed class MemberGroup : IMemberGroup
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="id">group id</param>
        /// <param name="memberType">.net member type</param>
        internal MemberGroup(Guid id, MemberTypes memberType)
        {
            Id = id;
            MemberType = memberType;
        }

        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public MemberTypes MemberType { get; }
    }
}
