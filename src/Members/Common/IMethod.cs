using NoRealm.Phi.Metadata.Extensions;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Represent method metadata
    /// </summary>
    public interface IMethod : IMethodBase, IInvokableMember
    {
        /// <summary>
        /// get .net <see cref="MethodInfo"/> instance
        /// </summary>
        new MethodInfo DotNetMember { get; }

        /// <summary>
        /// Get return value information
        /// </summary>
        IParameter ReturnInfo { get; }
    }
}
