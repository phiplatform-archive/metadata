using NoRealm.Phi.Metadata.CodeGeneration;
using NoRealm.Phi.Metadata.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent property information
    /// </summary>
    internal sealed class PropertyData : MemberData, IProperty
    {
        private readonly Func<object, object> get;
        private readonly Action<object, object> set;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="property">property information</param>
        /// <param name="memberConfiguration">member configuration</param>
        /// <param name="codeGenerator">runtime code generator</param>
        public PropertyData(PropertyInfo property, MemberConfiguration memberConfiguration, ICodeGenerator codeGenerator)
            : base(property, memberConfiguration)
        {
            if (codeGenerator == null)
                throw new ArgumentNullException(nameof(codeGenerator));

            var setMethod = property.SetMethod;
            var getMethod = property.GetMethod;

            DotNetMember = property;
            MemberGroup = MemberGroups.Property;
            Flags = property.Attributes;
            CanRead = property.CanRead;
            CanWrite = property.CanWrite;
            IsStatic = (CanRead && getMethod.IsStatic) || (CanWrite && setMethod.IsStatic);
            ReadContentType = property.CanRead? property.PropertyType: null;
            WriteContentType = property.CanWrite ? property.PropertyType : null;

            if (property.CanRead)
                ReadAttributes = getMethod.GetCustomAttributes().ToArray();

            if (property.CanWrite)
                WriteAttributes = setMethod.GetCustomAttributes().ToArray();

            set = codeGenerator.CreateSetter(property);
            get = codeGenerator.CreateGetter(property);
        }

        /// <inheritdoc/>
        public new PropertyInfo DotNetMember { get; }

        /// <inheritdoc/>
        public PropertyAttributes Flags { get; }

        /// <inheritdoc/>
        public Type ReadContentType { get; }

        /// <inheritdoc/>
        public Type WriteContentType { get; }

        /// <inheritdoc/>
        public bool CanRead { get; }

        /// <inheritdoc/>
        public bool CanWrite { get; }

        /// <inheritdoc/>
        public IReadOnlyList<Attribute> ReadAttributes { get; }

        /// <inheritdoc/>
        public IReadOnlyList<Attribute> WriteAttributes { get; }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetValue(IReadContext context) => get(context.Instance);

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(IWriteContext context) => set(context.Instance, context.Value);
    }
}
