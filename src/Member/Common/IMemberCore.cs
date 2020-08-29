using NoRealm.Phi.Shared.Utility;
using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// represent member core information
    /// </summary>
    public interface IMemberCore
    {
        /// <summary>
        /// get dotnet member type
        /// </summary>
        object DotNetMember { get; }

        /// <summary>
        /// Get <see cref="IMemberCore"/> which declared this member.
        /// </summary>
        IMemberCore DeclaringMember { get; }

        /// <summary>
        /// Get list of attributes applied to this member.
        /// </summary>
        IReadOnlyList<Attribute> Attributes { get; }

        /// <summary>
        /// Get member category.
        /// </summary>
        /// <remarks>if no member groups is available, the value will be <see cref="MemberGroups.None"/></remarks>
        IMemberGroup MemberGroup { get; }

        /// <summary>
        /// Get member name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get member hash code.
        /// </summary>
        /// <remarks>calculated using <see cref="StringExtensions.ComputeFnv1"/></remarks>
        ulong HashCode { get; }
    }
}
