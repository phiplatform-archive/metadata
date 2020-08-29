using NoRealm.Phi.Metadata.Extensions;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// represent method common information
    /// </summary>
    public interface IMethodBase : IMember, IParameters
    {
        /// <summary>
        /// get .net <see cref="MethodBase"/> instance
        /// </summary>
        new MethodBase DotNetMember { get; }


        /// <summary>
        /// get method characteristics
        /// </summary>
        MethodAttributes Flags { get; }
    }
}
