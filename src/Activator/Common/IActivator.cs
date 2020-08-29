using System;

namespace NoRealm.Phi.Metadata.Activator
{
    /// <summary>
    /// Represent a way for type activation
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        /// prepare type for later activation
        /// </summary>
        /// <param name="type">type information</param>
        /// <returns>true if type is ready, false if no provider support this type</returns>
        bool Prepare(Type type);

        /// <summary>
        /// create an instance of input type
        /// </summary>
        /// <param name="type">type information</param>
        /// <param name="args">argument to pass to the constructor, the order and type of the argument must match a provided constructor</param>
        /// <returns>an instance of input type; exception otherwise.</returns>
        /// <exception cref="ArgumentException">if no constructor found which match input arguments</exception>
        object CreateInstance(Type type, params object[] args);
    }
}
