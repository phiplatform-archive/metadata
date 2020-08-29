using NoRealm.Phi.Metadata.Members;
using NoRealm.Phi.Metadata.Test.Data;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class ConstantTests
    {
        private readonly IConstant constant =
            (IConstant) new DefaultMemberFactory(new MemberConfiguration(true)).CreateMember(
                typeof(User).GetField(nameof(User.MaxUsers)));

        [Fact]
        public void ConstValueIsCorrect()
        {
            Assert.Equal(User.MaxUsers, constant.Value);
        }
    }
}
