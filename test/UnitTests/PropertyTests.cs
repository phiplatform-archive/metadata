using System;
using NoRealm.Phi.Metadata.Extensions;
using NoRealm.Phi.Metadata.Members;
using NoRealm.Phi.Metadata.Test.Data;
using NoRealm.Phi.Metadata.Test.Fixture;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class PropertyTests : IClassFixture<StandardFlow>
    {
        public readonly IClass userClass;

        public PropertyTests(StandardFlow standardFlow)
        {
            userClass = (IClass)(standardFlow as IRequirement).RootMemberFactory.CreateRootMember(User.Type);
        }

        [Fact]
        public void WriteToPropertyIsValid()
        {
            var user = new User();
            var property = userClass.GetProperty(nameof(User.Name));
            var context = property.CreateWriteContext("testName", user);
            property.SetValue(context);

            Assert.Equal("testName", user.Name);
        }

        [Fact]
        public void ReadFromPropertyIsValid()
        {
            var user = new User{UserId = Guid.NewGuid()};
            var property = userClass.GetProperty(nameof(User.UserId));
            var context = property.CreateReadContext(user);

            Assert.Equal(property.GetValue(context), user.UserId);
        }

        [Fact]
        public void WriteToReadOnlyPropertyWillFail()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                var user = new User();
                var property = userClass.GetProperty(nameof(User.ReadOnly));
                var context = property.CreateWriteContext("testName", user);
                property.SetValue(context);
            });
        }

        [Fact]
        public void ReadFromWriteOnlyWillFail()
        {
            Assert.ThrowsAny<Exception>(() =>
            {
                var user = new User();
                var property = userClass.GetProperty(nameof(User.WriteOnly));
                var context = property.CreateReadContext(user);
                property.GetValue(context);
            });
        }
    }
}