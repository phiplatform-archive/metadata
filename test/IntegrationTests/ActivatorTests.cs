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

        public ActivatorTests(StandardFlow standardFlow)
        {
            activator = (standardFlow as IRequirement).Activator;
            activator.Prepare(User.Type);
        }

        [Fact]
        public void CreateInstanceNoArgs()
        {
            var instance = activator.CreateInstance(User.Type);
            Assert.NotNull(instance);
        }

        [Fact]
        public void CreateInstanceOneArgs()
        {
            var id = Guid.NewGuid();

            var instance = (User)activator.CreateInstance(User.Type, id);
            Assert.NotNull(instance);
            Assert.Equal(id, instance.userId);
        }

        [Fact]
        public void CreateInstanceTwoArgs()
        {
            var id = Guid.NewGuid();
            var name = "testName";

            var instance = (User)activator.CreateInstance(User.Type, id, name);
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
