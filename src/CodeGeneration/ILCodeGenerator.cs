using System;
using System.Reflection;
using System.Reflection.Emit;
using NoRealm.Phi.Metadata.Activator.Internal;

namespace NoRealm.Phi.Metadata.CodeGeneration
{
    /// <summary>
    /// code generation by emitting CIL code
    /// </summary>
    internal class ILCodeGenerator : ICodeGenerator
    {
        /// <inheritdoc />
        public Action<object, object> CreateSetter(MemberInfo member)
        {
            var method = CreateMethod($"set_{member.DeclaringType?.Name}_{member.Name}", 
                null, 
                typeof(object), typeof(object));

            var il = method.GetILGenerator();

            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    DefinePropertySetter(il, member as PropertyInfo);
                    break;
                case MemberTypes.Field:
                    DefineFieldSetter(il, member as FieldInfo);
                    break;
                default:
                    throw new NotSupportedException($"member type {member.MemberType} is not support");
            }

            return method.ToDelegate<Action<object, object>>();
        }

        /// <inheritdoc />
        public Func<object, object> CreateGetter(MemberInfo member)
        {
            var method = CreateMethod($"get_{member.DeclaringType?.Name}_{member.Name}",
                typeof(object),
                typeof(object));
            
            var il = method.GetILGenerator();

            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    DefinePropertyGetter(il, member as PropertyInfo);
                    break;
                case MemberTypes.Field:
                    DefineFieldGetter(il, member as FieldInfo);
                    break;
                default:
                    throw new NotSupportedException($"member type {member.MemberType} is not support");
            }

            return method.ToDelegate<Func<object, object>>();
        }

        /// <inheritdoc />
        public Func<object[], object> CreateActivator(ActivatorKey key, ConstructorInfo constructor)
        {
            var method = CreateMethod($"constructor_{key.Owner.Name}_{key.GetHashCode()}",
                typeof(object),
                typeof(object[]));

            var il = method.GetILGenerator();

            for (var i = 0; i < key.ParamTypes.Count; ++i)
                il.ldArg(0).ldcI4(i).ldElemRef().cast(key.ParamTypes[i]);

            il.newObj(constructor);

            if (constructor.DeclaringType.IsValueType)
                il.box(constructor.DeclaringType);

            il.ret();

            return method.ToDelegate<Func<object[], object>>();
        }

        /// <inheritdoc />
        public IndirectCall CreateInvokeMethod(MethodInfo methodInfo)
        {
            var method = CreateMethod($"method_{methodInfo.DeclaringType?.Name}_{methodInfo.Name}_{Guid.NewGuid():n}",
                typeof(object),
                typeof(object), typeof(object[]).MakeByRefType());

            var @params = methodInfo.GetParameters();

            var il = method.GetILGenerator();

            for (var i = 0; i < @params.Length; ++i)
            {
                var t = @params[i].ParameterType.IsByRef ? @params[i].ParameterType.GetElementType() : @params[i].ParameterType;

                il.DeclareLocal(t);

                il.ldArg(1)
                  .ldRefInd()
                  .ldcI4(i)
                  .ldElemRef()
                  .cast(t)
                  .stLocal(i);
            }

            if (!methodInfo.IsStatic)
                il.ldArg(0).cast(methodInfo.DeclaringType, true);

            for (var i = 0; i < @params.Length; ++i)
            {
                if (@params[i].ParameterType.IsByRef)
                    il.ldLocalAddr(i);
                else
                    il.ldLocal(i);
            }


            if (methodInfo.IsVirtual)
                il.callVirtual(methodInfo);
            else
                il.call(methodInfo);

            if (methodInfo.ReturnParameter.ParameterType == typeof(void))
                il.ldNull();

            il.ret();

            return method.ToDelegate<IndirectCall>();
        }

        private static void DefinePropertyGetter(ILGenerator il, PropertyInfo property)
        {
            var method = property.GetMethod;

            if (method == null)
                il.ThrowException(typeof(NotSupportedException));
            else
            {
                if (!method.IsStatic)
                    il.ldArg(0)
                      .cast(property.DeclaringType, true);

                if (method.IsVirtual)
                    il.callVirtual(method);
                else
                    il.call(method);

                if (property.PropertyType.IsValueType)
                    il.box(property.PropertyType);

                il.ret();
            }
        }

        private static void DefinePropertySetter(ILGenerator il, PropertyInfo property)
        {
            var method = property.SetMethod;

            if (method == null)
                il.ThrowException(typeof(NotSupportedException));
            else
            {
                if (!method.IsStatic)
                    il.ldArg(0)
                      .cast(property.DeclaringType, true);

                il.ldArg(1)
                  .cast(property.PropertyType);

                if (method.IsVirtual)
                    il.callVirtual(method);
                else
                    il.call(method);

                il.ret();
            }
        }

        private static void DefineFieldGetter(ILGenerator il, FieldInfo field)
        {
            if (!field.IsStatic)
                il.ldArg(0)
                  .cast(field.DeclaringType, true);

            il.ldField(field);

            if (field.FieldType.IsValueType)
                il.box(field.FieldType);

            il.ret();
        }
        
        private static void DefineFieldSetter(ILGenerator il, FieldInfo field)
        {
            if (field.IsInitOnly)
                il.ThrowException(typeof(NotSupportedException));
            else
            {
                if (!field.IsStatic)
                    il.ldArg(0)
                      .cast(field.DeclaringType, true);

                il.ldArg(1)
                  .cast(field.FieldType)
                  .stField(field)
                  .ret();
            }
        }

        private static DynamicMethod CreateMethod(string name, Type retType, params Type[] argTypes)
        {
            return new DynamicMethod(name,
                MethodAttributes.Public | MethodAttributes.Static,
                CallingConventions.Standard,
                retType,
                argTypes,
                typeof(ILCodeGenerator),
                true
           );
        }

    }
}
