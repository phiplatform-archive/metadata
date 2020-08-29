using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Extensions
{
    /// <summary>
    /// A context for invoking a member
    /// </summary>
    public interface IInvokeContext : IInstanceContext
    {
        /// <summary>
        /// get total number of parameters
        /// </summary>
        int Count { get; }

        /// <summary>
        /// get value of parameter at specified index
        /// </summary>
        /// <param name="position">parameter position</param>
        /// <returns>parameter value</returns>
        /// <exception cref="IndexOutOfRangeException">if <paramref name="position"/> is not found</exception>
        object this[int position] { get; }

        /// <summary>
        /// get value of parameter by parameter name
        /// </summary>
        /// <param name="paramName">parameter name</param>
        /// <returns>parameter value</returns>
        /// <exception cref="KeyNotFoundException">if <paramref name="paramName"/> is not found</exception>
        object this[string paramName] { get; }
    }
}
