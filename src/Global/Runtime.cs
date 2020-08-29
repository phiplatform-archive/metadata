using System.Runtime.CompilerServices;
using NoRealm.Phi.Metadata.CodeGeneration;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// provide access to global methods
    /// </summary>
    internal static class Runtime
    {
        /// <summary>
        /// dynamic code generator
        /// </summary>
        internal static readonly ICodeGenerator Generator = GetGenerator();

        /// <summary>
        /// create code generator
        /// </summary>
        private static ICodeGenerator GetGenerator()
        {
            if (RuntimeFeature.IsSupported("IsDynamicCodeCompiled"))
                return new ILCodeGenerator();
            else
                return new ExpressionCodeGenerator();
        }
    }
}
