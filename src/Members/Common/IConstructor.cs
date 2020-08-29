using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Represent constructor metadata
    /// </summary>
    public interface IConstructor : IMethodBase
    {
        /// <summary>
        /// get .net <see cref="ConstructorInfo"/> instance
        /// </summary>
        new ConstructorInfo DotNetMember { get; }

        /// <summary>
        /// Determine whether this constructor is default
        /// </summary>
        bool IsDefault { get; }
    }
}
