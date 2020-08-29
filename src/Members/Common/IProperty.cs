using NoRealm.Phi.Metadata.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Represent property metadata
    /// </summary>
    public interface IProperty : IMember, IReadableMember, IWritableMember
    {
        /// <summary>
        /// get .net <see cref="PropertyInfo"/> instance
        /// </summary>
        new PropertyInfo DotNetMember { get; }

        /// <summary>
        /// get property characteristics
        /// </summary>
        PropertyAttributes Flags { get; }

        /// <summary>
        /// get can read from property
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// get can write to property
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        /// Get list of attributes applied to Getter method.
        /// </summary>
        IReadOnlyList<Attribute> ReadAttributes { get; }

        /// <summary>
        /// Get list of attributes applied to Setter method.
        /// </summary>
        IReadOnlyList<Attribute> WriteAttributes { get; }
    }
}
