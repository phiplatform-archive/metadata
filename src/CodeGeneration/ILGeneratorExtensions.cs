using System;
using System.Reflection;
using System.Reflection.Emit;

namespace NoRealm.Phi.Metadata.CodeGeneration
{
    /// <summary>
    /// <see cref="ILGenerator"/> useful extensions
    /// </summary>
    internal static class ILGeneratorExtensions
    {
        /// <summary>
        /// generate delegate for a dynamic method
        /// </summary>
        /// <typeparam name="T">delegate type</typeparam>
        /// <param name="method">dynamic method</param>
        /// <returns>delegate to dynamic method</returns>
        internal static T ToDelegate<T>(this DynamicMethod method) where T: Delegate
        {
            return (T)method.CreateDelegate(typeof(T));
        }

        /// <summary>
        /// emit return instruction
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ret(this ILGenerator il)
        {
            il.Emit(OpCodes.Ret);
            return il;
        }

        /// <summary>
        /// execute reference type constructor, i.e. creating new object
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="constructor">constructor to call</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator newObj(this ILGenerator il, ConstructorInfo constructor)
        {
            il.Emit(OpCodes.Newobj, constructor);
            return il;
        }

        /// <summary>
        /// load argument at specified position
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="index">argument index</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldArg(this ILGenerator il, int index)
        {
            switch (index)
            {
                case 0:
                    il.Emit(OpCodes.Ldarg_0);
                    break;
                case 1:
                    il.Emit(OpCodes.Ldarg_1);
                    break;
                case 2:
                    il.Emit(OpCodes.Ldarg_2);
                    break;
                case 3:
                    il.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    il.Emit(OpCodes.Ldarg_S, index);
                    break;
            }

            return il;
        }

        /// <summary>
        /// call a method
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="method">method to call</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator call(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Call, method);
            return il;
        }

        /// <summary>
        /// call a virtual method
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="method">method to execute</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator callVirtual(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Callvirt, method);
            return il;
        }
        
        /// <summary>
        /// call a method using tail technique
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="method">method to call</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator callTail(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Tailcall);
            il.Emit(OpCodes.Call, method);
            return il;
        }

        /// <summary>
        /// call a virtual method using tail technique
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="method">method to execute</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator callVirtualTail(this ILGenerator il, MethodInfo method)
        {
            il.Emit(OpCodes.Tailcall);
            il.Emit(OpCodes.Callvirt, method);
            return il;
        }

        /// <summary>
        /// cast top of stack to input type
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="type">target type to cast into</param>
        /// <param name="asPointer">used with value type, when true specified unbox result is address of value type</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator cast(this ILGenerator il, Type type, bool asPointer = false)
        {
            if (type.IsValueType)
                il.Emit(asPointer ? OpCodes.Unbox : OpCodes.Unbox_Any, type);
            else if (type != typeof(object))
                il.Emit(OpCodes.Castclass, type);

            return il;
        }

        /// <summary>
        /// set value of field
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="fieldInfo">field to assign value</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator stField(this ILGenerator il, FieldInfo fieldInfo)
        {
            il.Emit(OpCodes.Stfld, fieldInfo);
            return il;
        }

        /// <summary>
        /// load value of field into stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="fieldInfo">field to load its value</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldField(this ILGenerator il, FieldInfo fieldInfo)
        {
            il.Emit(OpCodes.Ldfld, fieldInfo);
            return il;
        }

        /// <summary>
        /// create box of input type
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="type">box type</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator box(this ILGenerator il, Type type)
        {
            il.Emit(OpCodes.Box, type);
            return il;
        }

        /// <summary>
        /// load element reference into stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="type">type of the loaded element</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldElemRef(this ILGenerator il, Type type = null)
        {
            if (type == null)
                il.Emit(OpCodes.Ldelem_Ref);
            else
                il.Emit(OpCodes.Ldelem_Ref, type);

            return il;
        }

        /// <summary>
        /// load object reference into stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="type">type of the loaded element</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldRefInd(this ILGenerator il, Type type = null)
        {
            if (type == null)
                il.Emit(OpCodes.Ldind_Ref);
            else
                il.Emit(OpCodes.Ldind_Ref, type);

            return il;
        }

        /// <summary>
        /// load constant int8 into stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="value">constant value</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldcI4(this ILGenerator il, int value)
        {
            var arr = new[]
            {
                OpCodes.Ldc_I4_0, OpCodes.Ldc_I4_1, OpCodes.Ldc_I4_2,
                OpCodes.Ldc_I4_3, OpCodes.Ldc_I4_5, OpCodes.Ldc_I4_6,
                OpCodes.Ldc_I4_7, OpCodes.Ldc_I4_8
            };

            if (value <= 8)
                il.Emit(arr[value]);
            else if (value > 255)
                throw new ArgumentException("value must be less than 256.", nameof(value));
            else
                il.Emit(OpCodes.Ldc_I4_S, value);

            return il;
        }

        /// <summary>
        /// load null value into stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldNull(this ILGenerator il)
        {
            il.Emit(OpCodes.Ldnull);
            return il;
        }

        /// <summary>
        /// set local at index to value of top of stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="index">local index</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator stLocal(this ILGenerator il, int index)
        {
            switch (index)
            {
                case 0:
                    il.Emit(OpCodes.Stloc_0); break;
                case 1:
                    il.Emit(OpCodes.Stloc_1); break;
                case 2:
                    il.Emit(OpCodes.Stloc_2); break;
                case 3:
                    il.Emit(OpCodes.Stloc_3); break;
                default:
                    il.Emit(OpCodes.Stloc_S, index); break;
            }

            return il;
        }

        /// <summary>
        /// load content of local into stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="index">local index</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldLocal(this ILGenerator il, int index)
        {
            switch (index)
            {
                case 0:
                    il.Emit(OpCodes.Ldloc_0); break;
                case 1:
                    il.Emit(OpCodes.Ldloc_1); break;
                case 2:
                    il.Emit(OpCodes.Ldloc_2); break;
                case 3:
                    il.Emit(OpCodes.Ldloc_3); break;
                default:
                    il.Emit(OpCodes.Ldloc_S, index); break;
            }

            return il;
        }

        /// <summary>
        /// load address of local at specified index to stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <param name="index">local index</param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldLocalAddr(this ILGenerator il, int index)
        {
            il.Emit(OpCodes.Ldloca_S, index);
            return il;
        }

        /// <summary>
        /// load address of array element into stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator ldElemAddr(this ILGenerator il)
        {
            il.Emit(OpCodes.Ldelema);
            return il;
        }

        /// <summary>
        /// set array element value from top of the stack
        /// </summary>
        /// <param name="il"><see cref="ILGenerator"/> instance to emit <see cref="OpCode"/></param>
        /// <returns><see cref="ILGenerator"/> instance</returns>
        public static ILGenerator stElemRef(this ILGenerator il)
        {
            il.Emit(OpCodes.Stelem_Ref);
            return il;
        }
    }
}
