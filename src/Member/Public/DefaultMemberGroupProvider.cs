using System;
using System.Reflection;
using static NoRealm.Phi.Metadata.MemberGroups;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// A provider for known <see cref="MemberGroups"/>
    /// </summary>
    public class DefaultMemberGroupProvider : IMemberGroupProvider
    {
        /// <summary>
        /// Get <see cref="IMemberGroup"/> from <see cref="MemberTypes"/>
        /// </summary>
        /// <param name="memberTypes"><see cref="MemberTypes"/> value</param>
        /// <returns><see cref="IMemberGroup"/> instance, otherwise null if input member type not found</returns>
        public static IMemberGroup FromMemberTypes(MemberTypes memberTypes)
        {
            switch (memberTypes)
            {
                case MemberTypes.Constructor: return Constructor;
                case MemberTypes.Field: return Field;
                case MemberTypes.Property: return Property;
                case MemberTypes.Method: return Method;
                case MemberTypes.Event: return Event;
                default: return null;
            }
        }

        /// <inheritdoc />
        public virtual IMemberGroup GetMemberGroup(MemberInfo memberInfo)
        {
            var group = FromMemberTypes(memberInfo.MemberType);
            if (group == Field && ((FieldInfo)memberInfo).IsLiteral) return Constant;
            if (group != null) return group;

            if (memberInfo is Type type)
            {
                if (type.IsClass) return Class;
                if (type.IsInterface) return Interface;
                if (type.IsValueType && !type.IsEnum) return Structure;

                return null;
            }

            return null;
        }
    }
}
