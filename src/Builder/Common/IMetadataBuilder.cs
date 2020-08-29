using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Builder
{
    /// <summary>
    /// The metadata builder
    /// </summary>
    public interface IMetadataBuilder
    {
        #region content exclusion
        /// <summary>
        /// exclude all types in input name
        /// </summary>
        /// <param name="excludeGroup">group which this name represent</param>
        /// <param name="excludeName">the name to exclude</param>
        /// <returns>when done a reference to this instance</returns>
        /// <remarks><paramref name="excludeName"/> can end with * to indicate name pattern</remarks>
        IMetadataBuilder Exclude(ExcludeGroup excludeGroup, string excludeName);

        /// <summary>
        /// add types to be excluded
        /// </summary>
        /// <param name="types">types information</param>
        /// <returns>when done a reference to this instance</returns>
        IMetadataBuilder Exclude(params Type[] types);
        #endregion

        #region type registeration
        /// <summary>
        /// add types to the cache
        /// </summary>
        /// <param name="deepScan">
        /// when set to true all members of input type will be scanned and their types will get added into cache
        /// </param>
        /// <param name="types">types to insert into cache</param>
        /// <returns>when done a reference to this instance</returns>
        IMetadataBuilder AddTypes(bool deepScan, params Type[] types);
        #endregion

        #region metadata values

        /// <summary>
        /// add feature to root member
        /// </summary>
        /// <param name="featureType">feature type information</param>
        /// <param name="featureFactory">a function to create featureType instance</param>
        /// <param name="types">optional types to add features in</param>
        /// <returns>when done a reference to this instance</returns>
        /// <remarks>if types parameter not specified the featureFactory is used with all root members in the cache</remarks>
        IMetadataBuilder AddRootFeature(
            Type featureType,
            Func<IServiceProvider, IRootMember, object> featureFactory,
            params Type[] types
        );

        /// <summary>
        /// add feature to members of a root member
        /// </summary>
        /// <param name="featureType">feature type information</param>
        /// <param name="featureFactory">a function to create featureType instance</param>
        /// <param name="memberNames">optional tuple with types and the member names to add features in</param>
        /// <returns>when done a reference to this instance</returns>
        /// <remarks>
        /// if no tuple specified the feature will be added on all members, and 
        /// if members property of tuple is null or empty then feature will be added to all members of that type.
        /// only members of type which implement <see cref="IFeatures"/> will have the featureType instance.
        /// </remarks>
        IMetadataBuilder AddMemberFeature(
            Type featureType,
            Func<IServiceProvider, IFeatures, object> featureFactory,
            params (Type Type, IEnumerable<string> Members)[] memberNames
        );
        #endregion
    }
}
