using System;
using NoRealm.Phi.Metadata.Extensions;
using NoRealm.Phi.Metadata.Members;
using NoRealm.Phi.Metadata.Test.Data;
using NoRealm.Phi.Metadata.Test.Fixture;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class FieldTests : IClassFixture<StandardFlow>
    {
        public readonly IClass userClass;

        public FieldTests(StandardFlow standardFlow)
        {
            userClass = (IClass)(standardFlow as IRequirement).RootMemberFactory.CreateRootMember(typeof(User));
        }

        [Fact]
        public void WriteToFieldIsValid()
        {
            var user = new User();
            var field = userClass.GetField(nameof(User.name));
            var context = field.CreateWriteContext("testName", user);
            field.SetValue(context);

            Assert.Equal("testName", user.name);
        }

        [Fact]
        public void ReadFromFieldIsValid()
        {
            var user = new User{UserId = Guid.NewGuid()};
            var field = userClass.GetField(nameof(User.userId));
            var context = field.CreateReadContext(user);

            Assert.Equal(field.GetValue(context), user.userId);
        }

        [Fact]
        public void WriteToReadOnlyFieldWillFail()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                var user = new User();
                var field = userClass.GetField(nameof(User.SomeValue));
                var context = field.CreateWriteContext(15, user);
                field.SetValue(context);
            });
        }
    }
}
