using System;
using NoRealm.Phi.Metadata.Internal;

namespace NoRealm.Phi.Metadata.Extensions
{
    /// <summary>
    /// an entry point for creating read, write and invocation context
    /// </summary>
    public static class ContextExtensions
    {
        /// <summary>
        /// create a read context which bounded to specific member
        /// </summary>
        /// <param name="member">a member instance</param>
        /// <param name="instance">object instance; null if member is static</param>
        /// <returns>a context instance to pass into <see cref="IReadableMember.GetValue"/></returns>
        public static IReadContext CreateReadContext(this IReadableMember member, object instance = null)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            return new ReadContext(instance);
        }

        /// <summary>
        /// create a write context which bounded to specific member
        /// </summary>
        /// <param name="member">a member instance</param>
        /// <param name="value">a value to write</param>
        /// <param name="instance">object instance; null if member is static</param>
        /// <returns>a context instance to pass into <see cref="IWritableMember.SetValue"/></returns>
        public static IWriteContext CreateWriteContext(this IWritableMember member, object value,
            object instance = null)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            return new WriteContext(instance, value);
        }

        /// <summary>
        /// create an invocation context for non-generic member
        /// </summary>
        /// <param name="member">a member instance</param>
        /// <param name="instance">object instance; null if member is static</param>
        /// <param name="args">values to pass to method upon invocation</param>
        /// <returns>a context instance to pass into <see cref="IInvokableMember.Invoke"/></returns>
        public static IInvokeContext CreateInvokeContext(this IInvokableMember member, object instance = null,
            params object[] args)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (member.IsGeneric)
                throw new NotSupportedException($"invoking a generic method is not supported.");

            if (member.Parameters.Count != args.Length)
                throw new ArgumentOutOfRangeException(nameof(args), "number of arguments must be exact to number of parameters.");

            for (var i = 0; i < args.Length; ++i)
            {
                if (!member.Parameters[i].ContentType.IsInstanceOfType(args[i]))
                    throw new ArgumentException($"argument at position {i} has invalid type for parameter at same position.");
            }

            return new InvokeContext(member, instance, args);
        }
    }
}
