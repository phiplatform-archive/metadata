using NoRealm.Phi.Metadata.Members.Internal;
using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Default implementation for known members
    /// </summary>
    public class DefaultMemberFactory : IMemberFactory
    {
        /// <summary>member configuration</summary>
        protected readonly MemberConfiguration memberConfiguration;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="memberConfiguration">member configuration</param>
        public DefaultMemberFactory(MemberConfiguration memberConfiguration)
        {
            this.memberConfiguration = memberConfiguration ?? throw new ArgumentNullException(nameof(memberConfiguration));
        }

        /// <inheritdoc />
        public virtual IMemberCore CreateMember(MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));

            if (memberInfo is FieldInfo field)
            {
                if (field.IsLiteral) return new ConstantData(field, memberConfiguration);
                return new FieldData(field, memberConfiguration, Runtime.Generator);
            }

            if (memberInfo is PropertyInfo property)
                return new PropertyData(property, memberConfiguration, Runtime.Generator);

            if (memberInfo is ConstructorInfo constructor)
                return new ConstructorData(constructor, memberConfiguration);

            if (memberInfo is MethodInfo method)
                return new MethodData(method, memberConfiguration, Runtime.Generator);

            throw new NotSupportedException($"member {memberInfo.DeclaringType.FullName}.{memberInfo.Name} is not supported.");
        }
    }
}
