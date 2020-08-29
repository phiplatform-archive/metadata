using System;
using System.Collections.Generic;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Activator
{
    /// <summary>
    /// fetch all public constructor from non-generic type
    /// </summary>
    public sealed class PublicConstructorProvider : IConstructorProvider
    {
        /// <inheritdoc />
        public IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (type.IsClass || (type.IsValueType && !type.IsEnum))
                return type.GetConstructors();

            return new ConstructorInfo[0];
        }
    }
}
