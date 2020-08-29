using NoRealm.Phi.Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// Provide implementation for <see cref="IRootMember"/> and <see cref="IMemberCollection"/>
    /// </summary>
    internal abstract class RootMemberData : MemberData, IRootMember, IMemberCollection
    {
        /// <summary>members sorted by group</summary>
        protected readonly IDictionary<Guid, IMemberCore[]> membersGroups = new Dictionary<Guid, IMemberCore[]>();
        /// <summary>members sorted by name using input case</summary>
        protected readonly IDictionary<ulong, IMemberCore[]> normalNames = new Dictionary<ulong, IMemberCore[]>();
        /// <summary>members sorted by name using lower case</summary>
        protected readonly IDictionary<ulong, IMemberCore[]> lowerNames = new Dictionary<ulong, IMemberCore[]>();

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="type">type information</param>
        /// <param name="memberConfiguration">member configuration</param>
        /// <param name="memberSeq">members sequence</param>
        protected RootMemberData(Type type, MemberConfiguration memberConfiguration, IEnumerable<IMemberCore> memberSeq)
            : base(type, memberConfiguration)
        {
            DotNetMember = type;
            Id = type.GUID;
            Namespace = type.Namespace;
            FullName = type.FullName;
            QualifiedName = type.AssemblyQualifiedName;
            Module = type.Module;
            Flags = type.Attributes;

            // order members by type to optimize search
            Members = memberSeq.OrderBy(e => e.MemberGroup.Id).ToArray();

            if (Members.Count != 0)
            {
                foreach (var group in Members.GroupBy(e => e.MemberGroup.Id))
                    membersGroups.Add(group.Key, group.ToArray());

                foreach (var group in Members.GroupBy(e => e.HashCode))
                    normalNames.Add(group.Key, group.ToArray());

                foreach (var group in Members.GroupBy(e => e.Name.ToLower().ComputeFnv1()))
                    lowerNames.Add(group.Key, group.ToArray());
            }

            foreach (var member in Members)
            {
                switch (member)
                {
                    case MemberCoreData coreData:
                        coreData.DeclaringMember = this;
                        break;
                    case IUpdateDeclaringMember udm:
                        udm.UpdateDeclaringMember(this);
                        break;
                    default:
                        throw new MemberAccessException($"a custom member must implement interface {nameof(IUpdateDeclaringMember)}");
                }
            }
        }

        /// <inheritdoc/>
        public new Type DotNetMember { get; }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public string Namespace { get; }

        /// <inheritdoc/>
        public string FullName { get; }
        
        /// <inheritdoc/>
        public string QualifiedName { get; }
        
        /// <inheritdoc/>
        public Module Module { get; }
        
        /// <inheritdoc/>
        public TypeAttributes Flags { get; }
        
        /// <inheritdoc/>
        public IReadOnlyList<IMemberCore> Members { get; }

        /// <inheritdoc/>
        public IEnumerable<IMemberCore> GetMembersByGroup(IMemberGroup memberGroup)
        {
            if (memberGroup == null)
                throw new ArgumentNullException(nameof(memberGroup));

            return membersGroups.TryGetValue(memberGroup.Id, out var array) ? array : new IMemberCore[0];
        }

        /// <inheritdoc/>
        public IEnumerable<IMemberCore> GetMembersByName(string name, bool ignoreCase = false)
        {
            ulong hashCode;
            var dic = normalNames;

            if (!ignoreCase)
                hashCode = name.ComputeFnv1();
            else
            {
                dic = lowerNames;
                hashCode = name.ToLower().ComputeFnv1();
            }

            return dic.TryGetValue(hashCode, out var members)
                ? members
                : new IMember[] { };
        }

        /// <inheritdoc/>
        public IMemberCore GetMemberByName(string name, IMemberGroup memberGroup = null, bool ignoreCase = false)
        {
            var result = 
                GetMembersByName(name, ignoreCase)
                    .Where(e => memberGroup == null || e.MemberGroup.Id == memberGroup.Id)
                    .ToList();
            
            if (result.Count == 0) return null;
            
            if (result.Count > 1)
                throw new AmbiguousMatchException($"More than one member found with name '{name}'");

            return result[0];
        }
    }
}
