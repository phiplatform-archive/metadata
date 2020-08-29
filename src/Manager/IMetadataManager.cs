using NoRealm.Phi.Metadata.Builder;
using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Manager
{
    /// <summary>
    /// Represent metadata manager
    /// </summary>
    public interface IMetadataManager
    {
        #region type exclusion

        /// <summary>
        /// get list of excluded items
        /// </summary>
        /// <param name="excludeGroup">group of excluded items</param>
        /// <returns>a list of excluded items</returns>
        IReadOnlyList<string> GetExcluded(ExcludeGroup excludeGroup);

        /// <summary>
        /// get list of excluded types
        /// </summary>
        IReadOnlyList<Type> ExcludedTypes { get; }

        /// <summary>
        /// determine if type is excluded
        /// </summary>
        /// <param name="type">type information</param>
        /// <returns>true if type is excluded; false otherwise</returns>
        bool IsExcluded(Type type);

        #endregion

        #region type metadata

        /// <summary>
        /// add types to the cache
        /// </summary>
        /// <param name="deepScan">
        /// when set to true all members of input type will be scanned and their types will get added into cache
        /// </param>
        /// <param name="types">types to insert into cache</param>
        void AddTypes(bool deepScan, params Type[] types);

        /// <summary>
        /// get cached metadata
        /// </summary>
        /// <param name="type">type information</param>
        /// <returns>cached metadata; null if input type didn't get cached.</returns>
        IRootMember GetMetadata(Type type);

        #endregion
    }
}
