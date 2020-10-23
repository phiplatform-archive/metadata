using System;
using System.Linq;
using System.Reflection;
using NoRealm.Phi.Metadata.Test.Data;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class MembersProviderTest
    {
        private readonly IMembersProvider membersProvider = new DefaultMembersProvider();

        [Fact]
        public void PublicPropertiesAreValid()
        {
            Assert.Equal(User.Type.GetProperties().Length, GetCount<PropertyInfo>());
        }

        [Fact]
        public void PublicConstructorsAreValid()
        {
            Assert.Equal(User.Type.GetConstructors().Length, GetCount<ConstructorInfo>());
        }

        [Fact]
        public void PublicFieldsAreValid()
        {
            Assert.Equal(User.Type.GetFields().Length, GetCount<FieldInfo>());
        }

        //[Fact]
        //public void PublicEventsAreValid()
        //{
        //    Assert.Equal(userType.GetEvents().Length, GetCount<EventInfo>());
        //}

        [Fact]
        public void PublicMethodsAreValid()
        {
            Assert.Equal(User.Type.GetMethods().Count(e => !e.IsSpecialName), GetCount<MethodInfo>());
        }

        private int GetCount<T>() where T : MemberInfo
        {
            return membersProvider.GetMembers(User.Type).Count(e => e is T);
        }
    }
}