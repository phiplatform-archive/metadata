using NoRealm.Phi.Metadata.Test.Data;
using NoRealm.Phi.Metadata.Test.Fixture;
using System;
using System.Reflection;
using Xunit;
using static NoRealm.Phi.Metadata.MemberGroups;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class MemberGroupProviderTests
    {
        private readonly IMemberGroupProvider memberGroupProvider = new DefaultMemberGroupProvider();
        private readonly Type userType = typeof(User);

        [Fact]
        public void TypeIsClass()
        {
            Assert.Same(Class, GetMemberGroup(userType));
        }

        [Fact]
        public void TypeIsEvent()
        {
            var @event = userType.GetEvents()[0];

            Assert.Same(Event, GetMemberGroup(@event));
        }

        [Fact]
        public void TypeIsMethod()
        {
            var method = userType.GetMethods()[0];
            Assert.Same(Method, GetMemberGroup(method));
        }

        [Fact]
        public void TypeIsConstant()
        {
            var @const = userType.GetField(nameof(User.MaxUsers));
            Assert.Same(Constant, GetMemberGroup(@const));
        }

        [Fact]
        public void TypeIsConstructor()
        {
            var constructor = userType.GetConstructors()[0];
            Assert.Same(Constructor, GetMemberGroup(constructor));
        }

        [Fact]
        public void TypeIsProperty()
        {
            var property = userType.GetProperties()[0];
            Assert.Same(Property, GetMemberGroup(property));
        }

        [Fact]
        public void TypeIsField()
        {
            var field = userType.GetField(nameof(User.dob));
            Assert.Same(Field, GetMemberGroup(field));
        }

        [Fact]
        public void TypeIsStruct()
        {
            Assert.Same(Structure, GetMemberGroup(typeof(int)));
        }

        [Fact]
        public void TypeIsInterface()
        {
            Assert.Same(Interface, GetMemberGroup(typeof(IRequirement)));
        }

        private IMemberGroup GetMemberGroup(MemberInfo member)
            => memberGroupProvider.GetMemberGroup(member);
    }
}
