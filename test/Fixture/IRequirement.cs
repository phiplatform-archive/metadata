using NoRealm.Phi.Metadata.Activator;
using NoRealm.Phi.Metadata.Manager;

namespace NoRealm.Phi.Metadata.Test.Fixture
{
    /// <summary>
    /// represent the required instances to perform tests
    /// </summary>
    public interface IRequirement
    {
        /// <summary>
        /// get the metadata manager instance
        /// </summary>
        IMetadataManager MetadataManager => GetInstance<IMetadataManager>();

        /// <summary>
        /// get root member factory
        /// </summary>
        IRootMemberFactory RootMemberFactory => GetInstance<IRootMemberFactory>();

        /// <summary>
        /// get activator
        /// </summary>
        IActivator Activator => GetInstance<IActivator>();

        /// <summary>
        /// get an instance
        /// </summary>
        /// <typeparam name="T">type information</typeparam>
        /// <returns>the required instance.</returns>
        T GetInstance<T>() where T : class;
    }
}
