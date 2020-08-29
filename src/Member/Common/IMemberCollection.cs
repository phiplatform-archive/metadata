using System.Collections.Generic;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// Represent a collection of members
    /// </summary>
    public interface IMemberCollection
    {
        /// <summary>
        /// get members collection
        /// </summary>
        IReadOnlyList<IMemberCore> Members { get; }

        /// <summary>
        /// get members of specified <see cref="IMemberGroup"/>
        /// </summary>
        /// <param name="memberGroup">member group</param>
        /// <returns>a sequence of members of input group</returns>
        IEnumerable<IMemberCore> GetMembersByGroup(IMemberGroup memberGroup);

        /// <summary>
        /// get members by name
        /// </summary>
        /// <param name="name">member name</param>
        /// <param name="ignoreCase">set to true to ignore character casing</param>
        /// <returns>a sequence of members matching input name</returns>
        IEnumerable<IMemberCore> GetMembersByName(string name, bool ignoreCase = false);

        /// <summary>
        /// get member by name
        /// </summary>
        /// <param name="name">member name</param>
        /// <param name="memberGroup">optional member group</param>
        /// <param name="ignoreCase">set to true to ignore character casing</param>
        /// <returns>member information, null if member name not found</returns>
        IMemberCore GetMemberByName(string name, IMemberGroup memberGroup = null, bool ignoreCase = false);
    }
}
