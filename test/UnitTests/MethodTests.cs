using System.Linq;
using NoRealm.Phi.Metadata.Extensions;
using NoRealm.Phi.Metadata.Members;
using NoRealm.Phi.Metadata.Test.Data;
using NoRealm.Phi.Metadata.Test.Fixture;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class MethodTests : IClassFixture<StandardFlow>
    {
        public readonly IClass userClass;

        public MethodTests(StandardFlow standardFlow)
        {
            userClass = (IClass)(standardFlow as IRequirement).RootMemberFactory.CreateRootMember(typeof(User));
        }

        [Fact]
        public void GetMethodGroupIsValid()
        {
            var methods = userClass.GetMethod(nameof(User.GetOldAge));
            Assert.Equal(2, methods.Length);
        }

        [Fact]
        public void ExecuteMethodNoArgsIsValid()
        {
            var user = new User();

            var method = userClass.GetMethod(nameof(User.GetOldAge)).First(e => e.Parameters.Count == 0);
            var context = method.CreateInvokeContext(user);
            var result = method.Invoke(context);

            Assert.Equal(10, result);
        }

    }
}