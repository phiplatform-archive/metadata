using NoRealm.Phi.Metadata.Members;
using NoRealm.Phi.Metadata.Test.Data;
using NoRealm.Phi.Metadata.Test.Fixture;
using System;
using System.Collections.Generic;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.IntegrationTests
{
    public class RootMemberFactoryTests : IClassFixture<StandardFlow>
    {
        private readonly IRootMemberFactory rootMemberFactory;

        public RootMemberFactoryTests(StandardFlow standardFlow)
        {
            rootMemberFactory = (standardFlow as IRequirement).RootMemberFactory;
        }

        [Fact]
        public void TypeIsClass()
        {
            Assert.IsAssignableFrom<IClass>(rootMemberFactory.CreateRootMember(User.Type));
        }

        [Fact]
        public void TypeIsInterface()
        {
            Assert.IsAssignableFrom<IInterface>(rootMemberFactory.CreateRootMember(typeof(IRequirement)));
        }

        [Fact]
        public void TypeIsStructure()
        {
            Assert.IsAssignableFrom<IStructure>(rootMemberFactory.CreateRootMember(typeof(KeyValuePair<int, int>)));
        }

        [Fact]
        public void GenericTypeIsNotSupported()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                rootMemberFactory.CreateRootMember(typeof(Dictionary<,>));
            });
        }

        [Fact]
        public void OtherTypesThrowException()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                rootMemberFactory.CreateRootMember(typeof(Action));
            });
        }
    }
}
