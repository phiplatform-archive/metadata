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

        [Fact]
        public void TypeIsClass()
        {
            Assert.Same(Class, GetMemberGroup(User.Type));
        }

        [Fact]
        public void TypeIsEvent()
        {
            var @event = User.Type.GetEvents()[0];

            Assert.Same(Event, GetMemberGroup(@event));
        }

        [Fact]
        public void TypeIsMethod()
        {
            var method = User.Type.GetMethods()[0];
            Assert.Same(Method, GetMemberGroup(method));
        }

        [Fact]
        public void TypeIsConstant()
        {
            var @const = User.Type.GetField(nameof(User.MaxUsers));
            Assert.Same(Constant, GetMemberGroup(@const));
        }

        [Fact]
        public void TypeIsConstructor()
        {
            var constructor = User.Type.GetConstructors()[0];
            Assert.Same(Constructor, GetMemberGroup(constructor));
        }

        [Fact]
        public void TypeIsProperty()
        {
            var property = User.Type.GetProperties()[0];
            Assert.Same(Property, GetMemberGroup(property));
        }

        [Fact]
        public void TypeIsField()
        {
            var field = User.Type.GetField(nameof(User.dob));
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
