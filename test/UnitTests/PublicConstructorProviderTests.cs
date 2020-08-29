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
        private readonly Type userType = typeof(User);

        [Fact]
        public void PublicConstructorCountAreValid()
        {
            Assert.Equal(userType.GetConstructors().Length, provider.GetConstructors(userType).Count());
        }
    }
}
