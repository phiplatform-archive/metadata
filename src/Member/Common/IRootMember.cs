using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// Represent a top level member
    /// </summary>
    public interface IRootMember : IMember
    {
        /// <summary>
        /// get .net <see cref="Type"/>
        /// </summary>
        new Type DotNetMember { get; }

        /// <summary>
        /// get identifier
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// get namespace
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// get name include namespace
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// get member fully qualified name
        /// </summary>
        string QualifiedName { get; }

        /// <summary>
        /// get module containing this member
        /// </summary>
        Module Module { get; }

        /// <summary>
        /// get characteristics
        /// </summary>
        TypeAttributes Flags { get; }
    }
}
