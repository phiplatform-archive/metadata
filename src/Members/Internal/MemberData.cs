using NoRealm.Phi.Shared.Features;
using NoRealm.Phi.Shared.Utility;
using System;
using System.Linq;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent member information
    /// </summary>
    internal class MemberData : MemberCoreData, IMember
    {
        protected readonly MemberConfiguration memberConfiguration;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="member">member information</param>
        /// <param name="memberConfiguration">member configuration</param>
        protected MemberData(MemberInfo member, MemberConfiguration memberConfiguration)
        {
            this.memberConfiguration = memberConfiguration ?? throw new ArgumentNullException(nameof(memberConfiguration));

            DotNetMember = member ?? throw new ArgumentNullException(nameof(member));
            Attributes = member.GetCustomAttributes().ToArray();
            Name = member.Name;
            HashCode = member.Name.ComputeFnv1();
            Features = new FeatureCollection(memberConfiguration.DisposeFeatureOnRemove);
        }

        /// <inheritdoc/>
        public new MemberInfo DotNetMember { get; protected set; }

        /// <inheritdoc/>
        public new IRootMember DeclaringMember { get; protected internal set; }

        /// <inheritdoc/>
        public bool IsStatic { get; protected set; }

        /// <inheritdoc/>
        public IFeatureCollection Features { get; protected set; }
    }
}
