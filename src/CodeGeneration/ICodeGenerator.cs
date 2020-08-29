using NoRealm.Phi.Metadata.Activator.Internal;
using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata.CodeGeneration
{
    /// <summary>
    /// represent method signature for indirect method call
    /// </summary>
    /// <param name="instance">object instance</param>
    /// <param name="args">method arguments</param>
    /// <returns>method return value; null if method don't have return value</returns>
    internal delegate object IndirectCall(object instance, ref object[] args);

    /// <summary>
    /// runtime code generation
    /// </summary>
    internal interface ICodeGenerator
    {
        /// <summary>
        /// create setter method for input member
        /// </summary>
        /// <param name="member">member information</param>
        /// <returns>an action for setting member value</returns>
        Action<object, object> CreateSetter(MemberInfo member);

        /// <summary>
        /// create getter method for input member
        /// </summary>
        /// <param name="member">member information</param>
        /// <returns>a function for getting member value</returns>
        Func<object, object> CreateGetter(MemberInfo member);

        /// <summary>
        /// create type activator using input activator key
        /// </summary>
        /// <param name="key">activation information</param>
        /// <param name="constructor">constructor information</param>
        /// <returns>a function for creating type instance</returns>
        Func<object[], object> CreateActivator(ActivatorKey key, ConstructorInfo constructor);

        /// <summary>
        /// create a delegate for invoking a method
        /// </summary>
        /// <param name="method">method information</param>
        /// <returns>a method return value if any</returns>
        IndirectCall CreateInvokeMethod(MethodInfo method);
    }
}
