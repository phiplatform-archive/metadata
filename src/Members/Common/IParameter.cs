using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// represent method parameter
    /// </summary>
    public interface IParameter : IMemberCore
    {
        /// <summary>
        /// Get reflection instance for this member.
        /// </summary>
        new ParameterInfo DotNetMember { get; }

        /// <summary>
        /// Get <see cref="IMethodBase"/> which declared this parameter.
        /// </summary>
        new IMethodBase DeclaringMember { get; }

        /// <summary>
        /// Get parameter content type
        /// </summary>
        Type ContentType { get; }

        /// <summary>
        /// Determine whether the parameter type is by reference
        /// </summary>
        bool IsReference { get; }

        /// <summary>
        /// Get parameter direction
        /// </summary>
        ParameterDirection Direction { get; }

        /// <summary>
        /// get position in parameter list
        /// </summary>
        /// <remarks>-1 for <see cref="ParameterDirection.ReturnValue"/></remarks>
        int Position { get; }
    }
}
