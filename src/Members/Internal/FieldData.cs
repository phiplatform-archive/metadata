using NoRealm.Phi.Metadata.CodeGeneration;
using NoRealm.Phi.Metadata.Extensions;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent field information
    /// </summary>
    internal sealed class FieldData : MemberData, IField
    {
        private readonly Func<object, object> get;
        private readonly Action<object, object> set;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="field">field information</param>
        /// <param name="memberConfiguration">member configuration</param>
        /// <param name="codeGenerator">runtime code generator</param>
        public FieldData(FieldInfo field, MemberConfiguration memberConfiguration, ICodeGenerator codeGenerator)
            : base(field, memberConfiguration)
        {
            if (codeGenerator == null)
                throw new ArgumentNullException(nameof(codeGenerator));

            if (field.IsLiteral)
                throw new ArgumentException($"a constant field detected, please consider using {nameof(ConstantData)} instead.");

            DotNetMember = field;
            MemberGroup = MemberGroups.Field;
            ReadContentType = field.FieldType;
            WriteContentType = field.IsInitOnly ? null : field.FieldType;
            Flags = field.Attributes;
            IsReadOnly = field.IsInitOnly;
            IsStatic = field.IsStatic;

            set = codeGenerator.CreateSetter(field);
            get = codeGenerator.CreateGetter(field);
        }

        /// <inheritdoc/>
        public new FieldInfo DotNetMember { get; }

        /// <inheritdoc/>
        public FieldAttributes Flags { get; }
        
        /// <inheritdoc/>
        public bool IsReadOnly { get; }

        /// <inheritdoc/>
        public Type ReadContentType { get; }

        /// <inheritdoc/>
        public Type WriteContentType { get; }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetValue(IReadContext context) => get(context.Instance);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(IWriteContext context) => set(context.Instance, context.Value);
    }
}
