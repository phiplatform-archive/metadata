using System.Collections.Generic;
using NoRealm.Phi.Metadata.Members;

namespace NoRealm.Phi.Metadata.Extensions
{
    /// <summary>
    /// Represent a parameter list
    /// </summary>
    public interface IParameters
    {
        /// <summary>
        /// get parameters definition
        /// </summary>
        IReadOnlyList<IParameter> Parameters { get; }
    }
}
