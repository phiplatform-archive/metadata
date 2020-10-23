using NoRealm.Phi.Metadata.Activator;
using NoRealm.Phi.Metadata.Test.Data;
using System;
using System.Linq;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.UnitTests
{
    public class PublicConstructorProviderTests
    {
        private readonly IConstructorProvider provider = new PublicConstructorProvider();

        [Fact]
        public void PublicConstructorCountAreValid()
        {
            Assert.Equal(User.Type.GetConstructors().Length, provider.GetConstructors(User.Type).Count());
        }
    }
}
