using NoRealm.Phi.Metadata.Activator.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Activator
{
    /// <summary>
    /// a default implementation for <seealso cref="IActivator"/>
    /// </summary>
    public sealed class DefaultActivator : IActivator
    {
        private readonly IConstructorProvider[] providers;

        private readonly IDictionary<ActivatorKey, Func<object[], object>> cache
            = new Dictionary<ActivatorKey, Func<object[], object>>(
                Enumerable.Empty<KeyValuePair<ActivatorKey, Func<object[], object>>>(),
                new ActivatorKeyEqualityComparer());

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="providers">a sequence of providers</param>
        public DefaultActivator(IEnumerable<IConstructorProvider> providers)
        {
            if (providers == null)
                throw new ArgumentNullException(nameof(providers));

            this.providers = providers.ToArray();

            if (this.providers.Length == 0)
                throw new ArgumentException("at least one provider must exist.", nameof(providers));
        }

        /// <inheritdoc />
        public bool Prepare(Type type)
        {
            var set = new HashSet<ConstructorInfo>();

            foreach (var provider in providers)
                set.UnionWith(provider.GetConstructors(type));

            if (set.Count == 0)
                return false;

            foreach (var constructor in set)
            {
                var key = new ActivatorKey(type, constructor);

                if (cache.ContainsKey(key))
                    continue;

                cache.Add(key, Runtime.Generator.CreateActivator(key, constructor));
            }

            return true;
        }

        /// <inheritdoc />
        public object CreateInstance(Type type, params object[] args)
        {
            var key = new ActivatorKey(type, args);

            if (!cache.TryGetValue(key, out var f))
                throw new ArgumentException("no constructor matches the input argument signature.");

            return f(args);
        }
    }
}
