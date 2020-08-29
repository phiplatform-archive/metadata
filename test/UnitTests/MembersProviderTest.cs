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
        private readonly Type userType = typeof(User);

        [Fact]
        public void PublicPropertiesAreValid()
        {
            Assert.Equal(userType.GetProperties().Length, GetCount<PropertyInfo>());
        }

        [Fact]
        public void PublicConstructorsAreValid()
        {
            Assert.Equal(userType.GetConstructors().Length, GetCount<ConstructorInfo>());
        }

        [Fact]
        public void PublicFieldsAreValid()
        {
            Assert.Equal(userType.GetFields().Length, GetCount<FieldInfo>());
        }

        //[Fact]
        //public void PublicEventsAreValid()
        //{
        //    Assert.Equal(userType.GetEvents().Length, GetCount<EventInfo>());
        //}

        [Fact]
        public void PublicMethodsAreValid()
        {
            Assert.Equal(userType.GetMethods().Count(e => !e.IsSpecialName), GetCount<MethodInfo>());
        }

        private int GetCount<T>() where T : MemberInfo
        {
            return membersProvider.GetMembers(userType).Count(e => e is T);
        }
    }
}