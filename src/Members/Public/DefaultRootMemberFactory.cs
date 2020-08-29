using NoRealm.Phi.Metadata.Members.Internal;
using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Default implementation for creating root members
    /// </summary>
    public class DefaultRootMemberFactory : IRootMemberFactory
    {
        /// <summary>members provider for getting</summary>
        protected readonly IMembersProvider membersProvider;

        /// <summary>member factory for creating members</summary>
        protected readonly IMemberFactory memberFactory;

        /// <summary>member configuration</summary>
        protected readonly MemberConfiguration memberConfiguration;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="membersProvider">members provider for getting </param>
        /// <param name="memberFactory">member factory for creating members</param>
        /// <param name="memberConfiguration">member configuration</param>
        public DefaultRootMemberFactory(IMembersProvider membersProvider, IMemberFactory memberFactory, MemberConfiguration memberConfiguration)
        {
            this.membersProvider = membersProvider ?? throw new ArgumentNullException(nameof(membersProvider));
            this.memberFactory = memberFactory ?? throw new ArgumentNullException(nameof(memberFactory));
            this.memberConfiguration = memberConfiguration ?? throw new ArgumentNullException(nameof(memberConfiguration));
        }

        /// <inheritdoc />
        public virtual IRootMember CreateRootMember(Type type)
        {
            if (type.IsGenericTypeDefinition)
                throw new NotSupportedException("member must be non generic type");

            if (typeof(Delegate).IsAssignableFrom(type))
                throw new NotSupportedException("delegate types are not supported");

            if (type.IsValueType && type.IsEnum)
                throw new NotSupportedException("member must be a structure type");

            if (!type.IsValueType && !type.IsClass && !type.IsInterface)
                throw new NotSupportedException("member must be a class type or an interface type");

            var members = new HashSet<IMemberCore>();

            foreach (var member in membersProvider.GetMembers(type))
                members.Add(memberFactory.CreateMember(member));

            if (type.IsClass)
                return new ClassData(type, memberConfiguration, members);

            if (type.IsInterface)
                return new InterfaceData(type, memberConfiguration, members);

            return new StructureData(type, memberConfiguration, members);
        }
    }
}
