using System;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// a factory for creating root members
    /// </summary>
    public interface IRootMemberFactory
    {
        /// <summary>
        /// create root member instance
        /// </summary>
        /// <param name="type">type information</param>
        /// <returns><see cref="IRootMember"/> instance for input member.</returns>
        /// <exception cref="NotSupportedException">if input member is not supported.</exception>
        IRootMember CreateRootMember(Type type);
    }
}
