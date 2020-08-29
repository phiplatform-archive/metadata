using System;
using System.Collections.Generic;
using System.Linq;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Extension methods to support <see cref="IMemberCollection"/>
    /// </summary>
    public static class MemberCollectionExtensions
    {
        /// <summary>
        /// Get input <see cref="IMemberCollection"/> properties
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <returns>a sequence of properties; empty sequence otherwise</returns>
        public static IEnumerable<IProperty> GetProperties(this IMemberCollection memberCollection)
        {
            if (memberCollection == null) throw new ArgumentNullException(nameof(memberCollection));
            return memberCollection.GetMembersByGroup(MemberGroups.Property).Cast<IProperty>();
        }

        /// <summary>
        /// Get input <see cref="IMemberCollection"/> fields
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <returns>a sequence of fields; empty sequence otherwise</returns>
        public static IEnumerable<IField> GetFields(this IMemberCollection memberCollection)
        {
            if (memberCollection == null) throw new ArgumentNullException(nameof(memberCollection));
            return memberCollection.GetMembersByGroup(MemberGroups.Field).Cast<IField>();
        }

        /// <summary>
        /// Get input <see cref="IMemberCollection"/> constants
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <returns>a sequence of fields; empty sequence otherwise</returns>
        public static IEnumerable<IConstant> GetConstants(this IMemberCollection memberCollection)
        {
            if (memberCollection == null) throw new ArgumentNullException(nameof(memberCollection));
            return memberCollection.GetMembersByGroup(MemberGroups.Constant).Cast<IConstant>();
        }

        /// <summary>
        /// Get input <see cref="IMemberCollection"/> constructors
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <returns>a sequence of constructors; empty sequence otherwise</returns>
        public static IEnumerable<IConstructor> GetConstructors(this IMemberCollection memberCollection)
        {
            if (memberCollection == null) throw new ArgumentNullException(nameof(memberCollection));
            return memberCollection.GetMembersByGroup(MemberGroups.Constructor).Cast<IConstructor>();
        }

        /// <summary>
        /// Get input <see cref="IMemberCollection"/> methods
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <returns>a sequence of methods; empty sequence otherwise</returns>
        public static IEnumerable<IMethod> GetMethods(this IMemberCollection memberCollection)
        {
            if (memberCollection == null) throw new ArgumentNullException(nameof(memberCollection));
            return memberCollection.GetMembersByGroup(MemberGroups.Method).Cast<IMethod>();
        }

        /// <summary>
        /// Get property by name
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <param name="name">property name</param>
        /// <param name="ignoreCase">set to true to ignore character casing</param>
        /// <returns>property information, null if property name not found</returns>
        public static IProperty GetProperty(this IMemberCollection memberCollection, string name, bool ignoreCase = false)
            => memberCollection.GetMemberByName(name, MemberGroups.Property, ignoreCase) as IProperty;

        /// <summary>
        /// Get field by name
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <param name="name">field name</param>
        /// <param name="ignoreCase">set to true to ignore character casing</param>
        /// <returns>field information, null if field name not found</returns>
        public static IField GetField(this IMemberCollection memberCollection, string name, bool ignoreCase = false)
            => memberCollection.GetMemberByName(name, MemberGroups.Field, ignoreCase) as IField;

        /// <summary>
        /// Get constant by name
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <param name="name">constant name</param>
        /// <param name="ignoreCase">set to true to ignore character casing</param>
        /// <returns>constant information, null if constant name not found</returns>
        public static IConstant GetConstant(this IMemberCollection memberCollection, string name, bool ignoreCase = false)
            => memberCollection.GetMemberByName(name, MemberGroups.Constant, ignoreCase) as IConstant;

        /// <summary>
        /// Get methods by name
        /// </summary>
        /// <param name="memberCollection"><see cref="IMemberCollection"/> instance</param>
        /// <param name="name">method name</param>
        /// <param name="ignoreCase">set to true to ignore character casing</param>
        /// <returns>methods information, null if method name not found</returns>
        public static IMethod[] GetMethod(this IMemberCollection memberCollection, string name, bool ignoreCase = false)
            => memberCollection.GetMembersByName(name, ignoreCase).Where(e => e.MemberGroup == MemberGroups.Method)
                .Select(e => (IMethod) e).ToArray();
    }
}
