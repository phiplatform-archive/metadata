using System;

namespace NoRealm.Phi.Metadata.Extensions
{
    /// <summary>
    /// Represent a member which can be invoked
    /// </summary>
    public interface IInvokableMember : IParameters
    {
        /// <summary>
        /// determine whether the member is generic
        /// </summary>
        bool IsGeneric { get; }

        /// <summary>
        /// get member generic arguments
        /// </summary>
        Type[] GenericArguments { get; }

        /// <summary>
        /// invoke member
        /// </summary>
        /// <param name="context">invocation context</param>
        /// <returns>the method return value; null if method don't have return value</returns>
        object Invoke(IInvokeContext context);
    }
}
