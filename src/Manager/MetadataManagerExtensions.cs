using System;

namespace NoRealm.Phi.Metadata.Manager
{
    /// <summary>
    /// Extension methods to support <see cref="IMetadataManager"/>
    /// </summary>
    public static class MetadataManagerExtensions
    {
        /// <summary>
        /// determine if type is excluded
        /// </summary>
        /// <typeparam name="T">type name</typeparam>
        /// <param name="metadataManager"><see cref="IMetadataManager"/> instance</param>
        /// <returns>true if type is excluded; false otherwise</returns>
        public static bool IsExcluded<T>(this IMetadataManager metadataManager)
        {
            if (metadataManager == null)
                throw new ArgumentNullException(nameof(metadataManager));

            return metadataManager.IsExcluded(typeof(T));
        }

        /// <summary>
        /// get cached metadata
        /// </summary>
        /// <typeparam name="T">type name</typeparam>
        /// <returns>cached metadata; null if input type didn't get cached.</returns>
        public static IRootMember GetMetadata<T>(this IMetadataManager metadataManager)
        {
            if (metadataManager == null)
                throw new ArgumentNullException(nameof(metadataManager));

            return metadataManager.GetMetadata(typeof(T));
        }

        #region add types to cache

        /// <summary>
        /// add type to cache
        /// </summary>
        /// <typeparam name="T">type name</typeparam>
        /// <param name="metadataManager"><see cref="IMetadataManager"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataManager"/></returns>
        public static IMetadataManager AddType<T>(
            this IMetadataManager metadataManager,
            bool deepScan = false
        )
        {
            if (metadataManager == null)
                throw new ArgumentNullException(nameof(metadataManager));

            metadataManager.AddTypes(deepScan, typeof(T));
            return metadataManager;
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <param name="metadataManager"><see cref="IMetadataManager"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataManager"/></returns>
        public static IMetadataManager AddTypes<T1, T2>(
            this IMetadataManager metadataManager,
            bool deepScan = false
        )
        {
            if (metadataManager == null)
                throw new ArgumentNullException(nameof(metadataManager));

            metadataManager.AddTypes(deepScan, typeof(T1), typeof(T2));
            return metadataManager;
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <typeparam name="T3">type name</typeparam>
        /// <param name="metadataManager"><see cref="IMetadataManager"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataManager"/></returns>
        public static IMetadataManager AddTypes<T1, T2, T3>(
            this IMetadataManager metadataManager,
            bool deepScan = false
        )
        {
            if (metadataManager == null)
                throw new ArgumentNullException(nameof(metadataManager));

            metadataManager.AddTypes(deepScan, typeof(T1), typeof(T2), typeof(T3));
            return metadataManager;
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <typeparam name="T3">type name</typeparam>
        /// <typeparam name="T4">type name</typeparam>
        /// <param name="metadataManager"><see cref="IMetadataManager"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataManager"/></returns>
        public static IMetadataManager AddTypes<T1, T2, T3, T4>(
            this IMetadataManager metadataManager,
            bool deepScan = false
        )
        {
            if (metadataManager == null)
                throw new ArgumentNullException(nameof(metadataManager));

            metadataManager.AddTypes(deepScan, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
            return metadataManager;
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <typeparam name="T3">type name</typeparam>
        /// <typeparam name="T4">type name</typeparam>
        /// <typeparam name="T5">type name</typeparam>
        /// <param name="metadataManager"><see cref="IMetadataManager"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataManager"/></returns>
        public static IMetadataManager AddTypes<T1, T2, T3, T4, T5>(
            this IMetadataManager metadataManager,
            bool deepScan = false
        )
        {
            if (metadataManager == null)
                throw new ArgumentNullException(nameof(metadataManager));

            metadataManager.AddTypes(deepScan, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
            return metadataManager;
        }

        #endregion
    }
}
