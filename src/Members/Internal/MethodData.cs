using System;
using System.Reflection;
using NoRealm.Phi.Metadata.CodeGeneration;
using NoRealm.Phi.Metadata.Extensions;
using NoRealm.Phi.Metadata.Internal;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent method information
    /// </summary>
    internal class MethodData : MethodBase, IMethod
    {
        private readonly IndirectCall invoke;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="method">method information</param>
        /// <param name="memberConfiguration">member configuration</param>
        /// <param name="codeGenerator">runtime code generator</param>
        public MethodData(MethodInfo method, MemberConfiguration memberConfiguration, ICodeGenerator codeGenerator)
            : base(method, memberConfiguration)
        {
            DotNetMember = method;
            ReturnInfo = new ParameterData(method.ReturnParameter, this, true);
            IsGeneric = method.IsGenericMethod;
            GenericArguments = method.GetGenericArguments();

            invoke = codeGenerator.CreateInvokeMethod(method);
        }

        /// <inheritdoc/>
        public new MethodInfo DotNetMember { get; }

        /// <inheritdoc/>
        public IParameter ReturnInfo { get; }

        /// <inheritdoc/>
        public bool IsGeneric { get; }
        
        /// <inheritdoc/>
        public Type[] GenericArguments { get; }

        /// <inheritdoc/>
        public object Invoke(IInvokeContext context)
        {
            if (!(context is InvokeContext invokeContext))
                throw new ArgumentException(
                    $"invalid invoke context, please consider using {nameof(ContextExtensions.CreateInvokeContext)}.");

            return invoke(invokeContext.Instance, ref invokeContext.args);
        }
    }
}
