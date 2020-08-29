using System;
using NoRealm.Phi.Metadata.Activator;
using NoRealm.Phi.Metadata.Test.Data;
using NoRealm.Phi.Metadata.Test.Fixture;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.IntegrationTests
{
    public class ActivatorTests : IClassFixture<StandardFlow>
    {
        private readonly IActivator activator;
        private readonly Type userType = typeof(User);

        public ActivatorTests(StandardFlow standardFlow)
        {
            activator = (standardFlow as IRequirement).Activator;
            activator.Prepare(userType);
        }

        [Fact]
        public void CreateInstanceNoArgs()
        {
            var instance = activator.CreateInstance(userType);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CreateInstanceOneArgs()
        {
            var id = Guid.NewGuid();

            var instance = (User)activator.CreateInstance(userType, id);
            Assert.NotNull(instance);
            Assert.Equal(id, instance.userId);
        }

        [Fact]
        public void CreateInstanceTwoArgs()
        {
            var id = Guid.NewGuid();
            var name = "testName";

            var instance = (User)activator.CreateInstance(userType, id, name);
            Assert.NotNull(instance);
            Assert.Equal(id, instance.userId);
            Assert.Equal(name, instance.name);
        }

        [Fact]
        public void PareFailsForUnknownType()
        {
            Assert.False(activator.Prepare(typeof(AttributeTargets)));
            Assert.False(activator.Prepare(typeof(IRequirement)));
        }
    }
}
