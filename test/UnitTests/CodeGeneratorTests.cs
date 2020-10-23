using NoRealm.Phi.Metadata.CodeGeneration;
using NoRealm.Phi.Metadata.Test.Fixture;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class ILCodeGeneratorTests : CodeGeneratorCore
    {
        public ILCodeGeneratorTests() : base(new ILCodeGenerator())
        {
        }
    }

    public class ExpressionCodeGeneratorTests : CodeGeneratorCore
    {
        public ExpressionCodeGeneratorTests() : base(new ExpressionCodeGenerator())
        {
        }
    }
}
