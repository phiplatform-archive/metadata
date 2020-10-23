using System;
using NoRealm.Phi.Metadata.Members;
using NoRealm.Phi.Metadata.Test.Data;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class MemberFactoryTests
    {
        private readonly IMemberFactory memberFactory = new DefaultMemberFactory(new MemberConfiguration(true));

        [Fact]
        public void MemberIsField()
        {
            var field = User.Type.GetField(nameof(User.userId));
            Assert.IsAssignableFrom<IField>(memberFactory.CreateMember(field));
        }

        [Fact]
        public void MemberIsConstant()
        {
            var @const = User.Type.GetField(nameof(User.MaxUsers));
            Assert.IsAssignableFrom<IConstant>(memberFactory.CreateMember(@const));
        }

        [Fact]
        public void MemberIsProperty()
        {
            var property = User.Type.GetProperties()[0];
            Assert.IsAssignableFrom<IProperty>(memberFactory.CreateMember(property));
        }

        [Fact]
        public void MemberIsMethod()
        {
            var method = User.Type.GetMethod(nameof(User.OnUserCreated));
            Assert.IsAssignableFrom<IMethod>(memberFactory.CreateMember(method));
        }

        [Fact]
        public void MemberIsConstructor()
        {
            var constructor = User.Type.GetConstructors()[0];
            Assert.IsAssignableFrom<IConstructor>(memberFactory.CreateMember(constructor));
        }

        [Fact]
        public void CreateMemberThrowExceptionOnUnsupportedType()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                memberFactory.CreateMember(User.Type.GetEvents()[0]);
            });
        }
    }
}