using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Test
{
    /// <summary>
    /// reflection extension methods
    /// </summary>
    public static class ReflectionExtensions
    {
        public static PropertyInfo GetProperty<T>(Expression<Func<T, object>> action)
        {
            var expr = action.Body;

            if (expr is UnaryExpression ux)
                expr = ux.Operand;

            if (expr is MemberExpression me)
                return me.Member as PropertyInfo;

            return null;
        }

        public static PropertyInfo GetProperty<T>(string name)
        {
            return typeof(T).GetProperty(name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        public static FieldInfo GetField<T>(string name)
        {
            return typeof(T).GetField(name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        }

        public static FieldInfo GetField<T>(Expression<Func<T, object>> action)
        {
            var expr = action.Body;

            if (expr is UnaryExpression ux)
                expr = ux.Operand;

            if (expr is MemberExpression me)
                return me.Member as FieldInfo;

            return null;
        }

        private static MethodInfo GetMethod<T>(string name, dynamic[] argFlags, params Type[] types)
        {
            var methods = typeof(Type).GetMethods(BindingFlags.Public | BindingFlags.NonPublic
                                                                      | BindingFlags.Static | BindingFlags.Instance)
                .Where(e => e.Name == name);

            foreach (var method in methods)
            {
                var isValid = true;
                var ps = method.GetParameters().OrderBy(e => e.Position).ToArray();
                if (ps.Length != types.Length) continue;

                for (var i = 0; i < ps.Length; ++i)
                {
                    var type = ps[i].ParameterType;
                    if (type.IsByRef) type = type.GetElementType();

                    if (type != types[i])
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid) return method;
            }

            return null;
        }
    }
}
