using System;
using NoRealm.Phi.Metadata.Members;
using NoRealm.Phi.Metadata.Test.Data;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class MemberFactoryTests
    {
        private readonly IMemberFactory memberFactory = new DefaultMemberFactory(new MemberConfiguration(true));
        private readonly Type userType = typeof(User);

        [Fact]
        public void MemberIsField()
        {
            var field = userType.GetField(nameof(User.userId));
            Assert.IsAssignableFrom<IField>(memberFactory.CreateMember(field));
        }

        [Fact]
        public void MemberIsConstant()
        {
            var @const = userType.GetField(nameof(User.MaxUsers));
            Assert.IsAssignableFrom<IConstant>(memberFactory.CreateMember(@const));
        }

        [Fact]
        public void MemberIsProperty()
        {
            var property = userType.GetProperties()[0];
            Assert.IsAssignableFrom<IProperty>(memberFactory.CreateMember(property));
        }

        [Fact]
        public void MemberIsMethod()
        {
            var method = userType.GetMethod(nameof(User.OnUserCreated));
            Assert.IsAssignableFrom<IMethod>(memberFactory.CreateMember(method));
        }

        [Fact]
        public void MemberIsConstructor()
        {
            var constructor = userType.GetConstructors()[0];
            Assert.IsAssignableFrom<IConstructor>(memberFactory.CreateMember(constructor));
        }

        [Fact]
        public void CreateMemberThrowExceptionOnUnsupportedType()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                memberFactory.CreateMember(userType.GetEvents()[0]);
            });
        }
    }
}