using System.Reflection;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// Represent member information.
    /// </summary>
    public interface IMember : IMemberCore, IFeatures
    {
        /// <summary>
        /// Get .net <see cref="MemberInfo"/> instance
        /// </summary>
        new MemberInfo DotNetMember { get; }

        /// <summary>
        /// Get <see cref="IRootMember"/> which declared this member.
        /// </summary>
        new IRootMember DeclaringMember { get; }

        /// <summary>
        /// Get whether this member have static scope
        /// </summary>
        bool IsStatic { get; }
    }
}
