using NoRealm.Phi.Metadata.Extensions;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent constant information
    /// </summary>
    internal sealed class ConstantData : MemberData, IConstant
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="field">field information</param>
        /// <param name="memberConfiguration">member configuration</param>
        public ConstantData(FieldInfo field, MemberConfiguration memberConfiguration) : base(field, memberConfiguration)
        {
            if (!field.IsLiteral)
                throw new ArgumentException($"field {field.DeclaringType.Name}.{Name} is not a constant.");

            DotNetMember = field;
            MemberGroup = MemberGroups.Constant;
            IsStatic = field.IsStatic;

            ReadContentType = field.FieldType;
            Flags = field.Attributes;

            Value = field.GetRawConstantValue();
        }

        /// <inheritdoc/>
        public new FieldInfo DotNetMember { get; }

        /// <inheritdoc/>
        public FieldAttributes Flags { get; }

        /// <inheritdoc/>
        public object Value { get; }

        /// <inheritdoc/>
        public Type ReadContentType { get; }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetValue(IReadContext context) => Value;
    }
}
