using System;

namespace NoRealm.Phi.Metadata.Members
{
    /// <summary>
    /// provide a way to update <see cref="IMemberCore.DeclaringMember"/>
    /// </summary>
    public interface IUpdateDeclaringMember
    {
        /// <summary>
        /// set declaring member to input member then seal it
        /// </summary>
        /// <param name="memberCore">the declaring member</param>
        /// <exception cref="NotSupportedException">thrown when declaring member can not be updated.</exception>
        void UpdateDeclaringMember(IMemberCore memberCore);
    }
}
