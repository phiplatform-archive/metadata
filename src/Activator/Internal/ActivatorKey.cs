using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Activator.Internal
{
    /// <summary>
    /// a class to represent a composite key for several types
    /// </summary>
    internal class ActivatorKey
    {
        private readonly int hashCode;

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="owner">owner type</param>
        /// <param name="constructorInfo">a constructor to get parameters types</param>
        public ActivatorKey(Type owner, ConstructorInfo constructorInfo)
        {
            Owner = owner;
            ParamTypes = constructorInfo.GetParameters().Select(e => e.ParameterType).ToArray();
            hashCode = CalculateHashCode();
        }

        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="owner">owner type</param>
        /// <param name="values">values to get types</param>
        public ActivatorKey(Type owner, params object[] values)
        {
            Owner = owner;
            var types = new Type[values.Length];

            for (var i = 0; i < values.Length; ++i)
                types[i] = values[i].GetType();

            ParamTypes = types;
            hashCode = CalculateHashCode();
        }

        /// <summary>
        /// Get owner type
        /// </summary>
        public Type Owner { get; }

        /// <summary>
        /// Get parameters types
        /// </summary>
        public IReadOnlyList<Type> ParamTypes { get; }

        /// <summary>
        /// calculate hash code for this instance
        /// </summary>
        /// <returns>hash code for this instance</returns>
        private int CalculateHashCode()
        {
            var result = Owner.GetHashCode() * 0x33841D9;

            for (var i = 0; i < ParamTypes.Count; ++i)
            {
                result ^= ParamTypes[i].GetHashCode();
                result *= 0x33841D9;
            }

            return result;
        }


        /// <summary>
        /// get composite hash key using xor operation
        /// </summary>
        /// <returns>a hash key</returns>
        public override int GetHashCode() => hashCode;
    }
}
