using System.Reflection;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent constructor information
    /// </summary>
    internal class ConstructorData : MethodBase, IConstructor
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="constructor">constructor information</param>
        /// <param name="memberConfiguration">member configuration</param>
        public ConstructorData(ConstructorInfo constructor, MemberConfiguration memberConfiguration) : base(constructor, memberConfiguration)
        {
            DotNetMember = constructor;
            IsDefault = constructor.GetParameters().Length == 0;
            MemberGroup = MemberGroups.Constructor;
        }

        /// <inheritdoc/>
        public new ConstructorInfo DotNetMember { get; }

        /// <inheritdoc/>
        public bool IsDefault { get; }
    }
}
