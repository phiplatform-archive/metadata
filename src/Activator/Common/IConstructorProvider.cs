using System;
using System.Collections.Generic;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Activator
{
    /// <summary>
    /// provide type constructors
    /// </summary>
    public interface IConstructorProvider
    {
        /// <summary>
        /// get constructors which is supported by this provider
        /// </summary>
        /// <param name="type">type information</param>
        /// <returns>a sequence of constructors; empty sequence otherwise</returns>
        IEnumerable<ConstructorInfo> GetConstructors(Type type);
    }
}
