using NoRealm.Phi.Metadata.Extensions;
using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Internal
{
    /// <summary>
    /// a context for invoking members
    /// </summary>
    internal sealed class InvokeContext : IInvokeContext
    {
        private readonly IInvokableMember member;
        internal object[] args; // don't make it readonly so we can use it with dynamic invoke

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="member">member information</param>
        /// <param name="instance">object instance; null if member is static</param>
        /// <param name="args">values to pass to method upon invocation</param>
        internal InvokeContext(IInvokableMember member, object instance, object[] args)
        {
            this.member = member;
            this.args = args;
            Instance = instance;
        }

        /// <inheritdoc />
        public object Instance { get; }

        /// <inheritdoc />
        public int Count => member.Parameters.Count;

        /// <inheritdoc />
        public object this[int position]
        {
            get
            {
                if (position < 0 || position > member.Parameters.Count)
                    throw new IndexOutOfRangeException();

                return args[position];
            }
        }

        /// <inheritdoc />
        public object this[string paramName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(paramName))
                    throw new KeyNotFoundException();

                foreach (var parameter in member.Parameters)
                {
                    if (parameter.Name == paramName)
                        return this[parameter.Position];
                }

                throw new KeyNotFoundException();
            }
        }
    }
}
