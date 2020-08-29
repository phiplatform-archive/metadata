using System;
using System.Linq.Expressions;
using System.Reflection;
using NoRealm.Phi.Metadata.Activator.Internal;

namespace NoRealm.Phi.Metadata.CodeGeneration
{
    /// <summary>
    /// code generation using LINQ expressions
    /// </summary>
    internal class ExpressionCodeGenerator : ICodeGenerator
    {
        /// <inheritdoc />
        public Action<object, object> CreateSetter(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    return DefinePropertySetter(member as PropertyInfo);
                case MemberTypes.Field:
                    return DefineFieldSetter(member as FieldInfo);
                default:
                    throw new NotSupportedException($"member type {member.MemberType} is not support");
            }
        }

        /// <inheritdoc />
        public Func<object, object> CreateGetter(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    return DefinePropertyGetter(member as PropertyInfo);
                case MemberTypes.Field:
                    return DefineFieldGetter(member as FieldInfo);
                default:
                    throw new NotSupportedException($"member type {member.MemberType} is not support");
            }
        }

        /// <inheritdoc />
        public Func<object[], object> CreateActivator(ActivatorKey key, ConstructorInfo constructor)
        {
            var argument = Expression.Parameter(typeof(object[]));

            var expressions = new Expression[key.ParamTypes.Count];

            for (var i = 0; i < key.ParamTypes.Count; ++i)
            {
                expressions[i] = Expression.ArrayAccess(argument, Expression.Constant(i));
                expressions[i] = Expression.Convert(expressions[i], key.ParamTypes[i]);
            }

            var ctor = Expression.New(constructor, expressions);

            return Expression
                .Lambda<Func<object[], object>>(ctor, argument)
                .Compile();
        }

        /// <inheritdoc />
        public IndirectCall CreateInvokeMethod(MethodInfo method)
        {
            var instance = Expression.Parameter(typeof(object));
            var argument = Expression.Parameter(typeof(object[]).MakeByRefType());
            var @params = method.GetParameters();

            var expressions = new Expression[@params.Length];

            for (var i = 0; i < @params.Length; ++i)
            {
                expressions[i] = Expression.ArrayAccess(argument, Expression.Constant(i));

                if (!@params[i].ParameterType.ContainsGenericParameters)
                    expressions[i] = Expression.Convert(expressions[i], @params[i].ParameterType);
            }

            Expression last = Expression.Call(instance, method, argument);

            if (method.ReturnParameter.ParameterType == typeof(void))
            {
                last = Expression.Block(last,
                    Expression.Label(Expression.Label(typeof(object)), Expression.Constant(null))
                );
            }
            else if (method.ReturnParameter.ParameterType != typeof(object))
            {
                last = Expression.Convert(last, typeof(object));
            }

            return Expression
                .Lambda<IndirectCall>(last, argument)
                .Compile();
        }


        private static Func<object, object> DefinePropertyGetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object));

            if (!property.CanRead)
            {
                return e =>
                    throw new NotSupportedException($"property {property.DeclaringType.Name}.{property.Name} is write only.");
            }
            else
            {
                var method = property.GetMethod;

                var getMethodExpr = Expression.Call(
                    method.IsStatic? null : Expression.Convert(instance, property.DeclaringType),
                    method
                );

                return Expression
                    .Lambda<Func<object, object>>(Expression.Convert(getMethodExpr, typeof(object)), instance)
                    .Compile();
            }
        }

        private static Action<object, object> DefinePropertySetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object));
            var argument = Expression.Parameter(typeof(object));

            if (!property.CanWrite)
            {
                return (a, b) =>
                    throw new NotSupportedException($"property {property.DeclaringType.Name}.{property.Name} is read only.");
            }
            else
            {
                var method = property.SetMethod;

                var setMethodExpr = Expression.Call(
                    method.IsStatic? null: Expression.Convert(instance, property.DeclaringType),
                    method,
                    Expression.Convert(argument, property.PropertyType));

                return Expression
                    .Lambda<Action<object, object>>(setMethodExpr, instance, argument)
                    .Compile();
            }
        }

        private static Func<object, object> DefineFieldGetter(FieldInfo fieldInfo)
        {
            var instance = Expression.Parameter(typeof(object));
            var fieldExpr = Expression.Field(
                fieldInfo.IsStatic? null : Expression.Convert(instance, fieldInfo.DeclaringType),
                fieldInfo);

            return Expression
                .Lambda<Func<object, object>>(Expression.Convert(fieldExpr, typeof(object)), instance)
                .Compile();
        }

        private static Action<object, object> DefineFieldSetter(FieldInfo fieldInfo)
        {
            var instance = Expression.Parameter(typeof(object));
            var argument = Expression.Parameter(typeof(object));

            if (fieldInfo.IsInitOnly)
            {
                return (a, b) =>
                    throw new NotSupportedException($"field {fieldInfo.DeclaringType.Name}.{fieldInfo.Name} is read only.");
            }
            else
            {
                var fieldExpr = Expression.Field(
                    fieldInfo.IsStatic? null : Expression.Convert(instance, fieldInfo.DeclaringType),
                    fieldInfo);

                var assignExpr = Expression.Assign(fieldExpr, Expression.Convert(argument, fieldInfo.FieldType));
                return Expression.Lambda<Action<object, object>>(assignExpr, instance, argument).Compile();
            }
        }
    }
}
