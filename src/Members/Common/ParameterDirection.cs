using System;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// Direction of parameter content
    /// </summary>
    [Flags]
    public enum ParameterDirection
    {
        /// <summary>
        /// Content is passed to the parameter into the method
        /// </summary>
        In = 1,

        /// <summary>
        /// Content is passed from the method to the caller
        /// </summary>
        Out = 2,

        /// <summary>
        /// Content is a method return value
        /// </summary>
        ReturnValue = 4
    }
}
