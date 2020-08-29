using System.Reflection;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// A provider for member groups
    /// </summary>
    public interface IMemberGroupProvider
    {
        /// <summary>
        /// Get <see cref="IMemberGroup"/> that matches <see cref="MemberInfo"/>
        /// </summary>
        /// <param name="memberInfo">member information</param>
        /// <returns><see cref="IMemberGroup"/> instance, otherwise null if group for input member not found</returns>
        IMemberGroup GetMemberGroup(MemberInfo memberInfo);
    }
}
