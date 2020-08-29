using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Builder
{
    /// <summary>
    /// Extension methods to support <see cref="IMetadataBuilder"/>
    /// </summary>
    public static class MetadataBuilderExtensions
    {
        #region exclusion

        /// <summary>
        /// exclude one or more assembly name
        /// </summary>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="assemblyNames">one or more assembly name</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder ExcludeAssembly(this IMetadataBuilder builder,
            params string[] assemblyNames)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (assemblyNames == null)
                throw new ArgumentNullException(nameof(assemblyNames));

            foreach (var assemblyName in assemblyNames)
                builder.Exclude(ExcludeGroup.Assembly, assemblyName);

            return builder;
        }
        
        /// <summary>
        /// exclude one or more module name
        /// </summary>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="moduleNames">one or more module name</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder ExcludeModule(this IMetadataBuilder builder,
            params string[] moduleNames)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (moduleNames == null)
                throw new ArgumentNullException(nameof(moduleNames));

            foreach (var moduleName in moduleNames)
                builder.Exclude(ExcludeGroup.Module, moduleName);

            return builder;
        }

        /// <summary>
        /// exclude one or more assembly
        /// </summary>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="assemblies">one or more assembly reference</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder ExcludeAssembly(this IMetadataBuilder builder,
            params Assembly[] assemblies)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            foreach (var assembly in assemblies)
                builder.Exclude(ExcludeGroup.Assembly, assembly.GetName().Name);

            return builder;
        }

        /// <summary>
        /// exclude one or more module
        /// </summary>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="modules">one or more module reference</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder ExcludeAssembly(this IMetadataBuilder builder,
            params Module[] modules)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (modules == null)
                throw new ArgumentNullException(nameof(modules));

            foreach (var module in modules)
                builder.Exclude(ExcludeGroup.Module, module.Name);

            return builder;
        }
        
        /// <summary>
        /// exclude one or more namespace
        /// </summary>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="namespaces">one or more namespace</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder ExcludeNamespace(this IMetadataBuilder builder,
            params string[] namespaces)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (namespaces == null)
                throw new ArgumentNullException(nameof(namespaces));

            foreach (var @namespace in namespaces)
                builder.Exclude(ExcludeGroup.Namespace, @namespace);

            return builder;
        }

        /// <summary>
        /// add type to be excluded
        /// </summary>
        /// <typeparam name="T">type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder Exclude<T>(this IMetadataBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            return builder.Exclude(typeof(T));
        }

        /// <summary>
        /// add types to be excluded
        /// </summary>
        /// <typeparam name="T1">type name.</typeparam>
        /// <typeparam name="T2">type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder Exclude<T1, T2>(this IMetadataBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.Exclude(typeof(T1), typeof(T2));
        }

        /// <summary>
        /// add types to be excluded
        /// </summary>
        /// <typeparam name="T1">type name.</typeparam>
        /// <typeparam name="T2">type name.</typeparam>
        /// <typeparam name="T3">type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder Exclude<T1, T2, T3>(this IMetadataBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.Exclude(typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// add types to be excluded
        /// </summary>
        /// <typeparam name="T1">type name.</typeparam>
        /// <typeparam name="T2">type name.</typeparam>
        /// <typeparam name="T3">type name.</typeparam>
        /// <typeparam name="T4">type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder Exclude<T1, T2, T3, T4>(this IMetadataBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.Exclude(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// add types to be excluded
        /// </summary>
        /// <typeparam name="T1">type name.</typeparam>
        /// <typeparam name="T2">type name.</typeparam>
        /// <typeparam name="T3">type name.</typeparam>
        /// <typeparam name="T4">type name.</typeparam>
        /// <typeparam name="T5">type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder Exclude<T1, T2, T3, T4, T5>(this IMetadataBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.Exclude(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        #endregion

        #region default exclusion

        /// <summary>
        /// add known namespaces to exclusion list
        /// </summary>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        /// <remarks>the namespaces are System.* and Microsoft.*</remarks>
        public static IMetadataBuilder ExcludeDefaultNamespaces(this IMetadataBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.ExcludeNamespace(
                "System",
                "System.*",
                "Microsoft",
                "Microsoft.*"
            );
        }

        #endregion

        #region add types to cache

        /// <summary>
        /// add type to cache
        /// </summary>
        /// <typeparam name="T">type name</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddType<T>(
            this IMetadataBuilder builder,
            bool deepScan = false
        )
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTypes(deepScan, typeof(T));
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddTypes<T1, T2>(
            this IMetadataBuilder builder,
            bool deepScan = false
        )
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTypes(deepScan, typeof(T1), typeof(T2));
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <typeparam name="T3">type name</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddTypes<T1, T2, T3>(
            this IMetadataBuilder builder,
            bool deepScan = false
        )
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTypes(deepScan, typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <typeparam name="T3">type name</typeparam>
        /// <typeparam name="T4">type name</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddTypes<T1, T2, T3, T4>(
            this IMetadataBuilder builder,
            bool deepScan = false
        )
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTypes(deepScan, typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// add types to cache
        /// </summary>
        /// <typeparam name="T1">type name</typeparam>
        /// <typeparam name="T2">type name</typeparam>
        /// <typeparam name="T3">type name</typeparam>
        /// <typeparam name="T4">type name</typeparam>
        /// <typeparam name="T5">type name</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="deepScan">
        /// when set to true if a member have content then it will be scanned to be added into cache
        /// </param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddTypes<T1, T2, T3, T4, T5>(
            this IMetadataBuilder builder,
            bool deepScan = false
        )
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddTypes(deepScan, typeof(T1), typeof(T2), typeof(T3), typeof(T4),
                typeof(T5));
        }

        #endregion

        #region add features to root members

        /// <summary>
        /// add feature to all root members
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToAllRootMembers<TX>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IRootMember, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddRootFeature(typeof(TX), featureFactory);
        }

        /// <summary>
        /// add feature to specific root member
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToRootMember<TX, T>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IRootMember, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddRootFeature(typeof(TX), featureFactory, typeof(T));
        }
        
        /// <summary>
        /// add feature to specific root members
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToRootMember<TX, T1, T2>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IRootMember, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddRootFeature(typeof(TX), featureFactory, typeof(T1),
                typeof(T2));
        }

        /// <summary>
        /// add feature to specific root members
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <typeparam name="T3">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToRootMember<TX, T1, T2, T3>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IRootMember, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddRootFeature(typeof(TX), featureFactory, typeof(T1),
                typeof(T2), typeof(T3));
        }

        /// <summary>
        /// add feature to specific root members
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <typeparam name="T3">target type name.</typeparam>
        /// <typeparam name="T4">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToRootMember<TX, T1, T2, T3, T4>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IRootMember, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddRootFeature(typeof(TX), featureFactory, typeof(T1),
                typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// add feature to specific root members
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <typeparam name="T3">target type name.</typeparam>
        /// <typeparam name="T4">target type name.</typeparam>
        /// <typeparam name="T5">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToRootMember<TX, T1, T2, T3, T4, T5>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IRootMember, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddRootFeature(typeof(TX), featureFactory, typeof(T1),
                typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        #endregion

        #region add features to members

        /// <summary>
        /// add feature to all members of a root member
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToAllMembers<TX>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IFeatures, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddMemberFeature(typeof(TX), featureFactory);
        }

        /// <summary>
        /// add feature to all members of specific type
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToTypeMembers<TX, T>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IFeatures, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddMemberFeature(typeof(TX), featureFactory, (typeof(T), null));
        }

        /// <summary>
        /// add feature to all members of specific types
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToTypeMembers<TX, T1, T2>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IFeatures, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddMemberFeature(typeof(TX),
                featureFactory,
                (typeof(T1), null), (typeof(T2), null));
        }

        /// <summary>
        /// add feature to all members of specific types
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <typeparam name="T3">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToTypeMembers<TX, T1, T2, T3>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IFeatures, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddMemberFeature(typeof(TX),
                featureFactory,
                (typeof(T1), null), (typeof(T2), null), (typeof(T3), null));
        }

        /// <summary>
        /// add feature to all members of specific types
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <typeparam name="T3">target type name.</typeparam>
        /// <typeparam name="T4">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToTypeMembers<TX, T1, T2, T3, T4>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IFeatures, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddMemberFeature(typeof(TX),
                featureFactory,
                (typeof(T1), null), (typeof(T2), null), (typeof(T3), null),
                (typeof(T4), null));
        }

        /// <summary>
        /// add feature to all members of specific types
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T1">target type name.</typeparam>
        /// <typeparam name="T2">target type name.</typeparam>
        /// <typeparam name="T3">target type name.</typeparam>
        /// <typeparam name="T4">target type name.</typeparam>
        /// <typeparam name="T5">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        public static IMetadataBuilder AddFeatureToTypeMembers<TX, T1, T2, T3, T4, T5>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IFeatures, TX> featureFactory
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddMemberFeature(typeof(TX),
                featureFactory,
                (typeof(T1), null), (typeof(T2), null), (typeof(T3), null),
                (typeof(T4), null), (typeof(T5), null));
        }

        /// <summary>
        /// add feature to specific type members
        /// </summary>
        /// <typeparam name="TX">feature type.</typeparam>
        /// <typeparam name="T">target type name.</typeparam>
        /// <param name="builder"><see cref="IMetadataBuilder"/> instance</param>
        /// <param name="featureFactory">a function to create an instance of the feature</param>
        /// <param name="membersSelector">a selector function to get members of type</param>
        /// <returns>when done a reference to input <see cref="IMetadataBuilder"/></returns>
        /// <remarks>member selector must be a single return statement with single member or array of members</remarks>
        public static IMetadataBuilder AddFeatureToSpecificTypeMembers<TX, T>(
            this IMetadataBuilder builder,
            Func<IServiceProvider, IFeatures, TX> featureFactory,
            Expression<Func<T, object>> membersSelector
        ) where TX : class
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            var list = new List<string>();

            if (membersSelector.Body is NewArrayExpression array)
            {
                foreach (var exp in array.Expressions)
                    list.Add(GetName(exp));
            }
            else
                list.Add(GetName(membersSelector.Body));

            if (list.Count == 0)
                throw new ArgumentException($"at least one member must be specified.", nameof(membersSelector));

            return builder.AddMemberFeature(typeof(TX), featureFactory, (typeof(T), list.ToArray()));

            string GetName(Expression ex)
            {
                if (ex.NodeType == ExpressionType.Convert)
                    ex = ((UnaryExpression)ex).Operand;

                if (ex.NodeType == ExpressionType.MemberAccess)
                {
                    var member = ((MemberExpression) ex).Member;

                    if (member.DeclaringType == typeof(T))
                        return member.Name;
                }

                throw new NotSupportedException($"unsupported expression found with value '{ex}'");
            }
        }

        #endregion
    }
}
