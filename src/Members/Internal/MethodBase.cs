using System.Collections.Generic;
using System.Reflection;
using DotNetMB = System.Reflection.MethodBase;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent method core information
    /// </summary>
    internal class MethodBase : MemberData, IMethodBase
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="method">method information</param>
        /// <param name="memberConfiguration">member configuration</param>
        public MethodBase(DotNetMB method, MemberConfiguration memberConfiguration) : base(method, memberConfiguration)
        {
            DotNetMember = method;
            Flags = method.Attributes;
            MemberGroup = MemberGroups.Method;
            IsStatic = method.IsStatic;

            var nativeParams = method.GetParameters();
            var @params = new ParameterData[nativeParams.Length];

            for (var i = 0; i < nativeParams.Length; ++i)
                @params[i] = new ParameterData(nativeParams[i], this);

            Parameters = @params;
        }

        /// <inheritdoc/>
        public new DotNetMB DotNetMember { get; }

        /// <inheritdoc/>
        public MethodAttributes Flags { get; }
        
        /// <inheritdoc/>
        public IReadOnlyList<IParameter> Parameters { get; }
    }
}
