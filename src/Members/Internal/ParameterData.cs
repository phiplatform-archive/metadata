using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata.Members.Internal
{
    /// <summary>
    /// represent parameter information
    /// </summary>
    internal class ParameterData : MemberCoreData, IParameter
    {
        /// <summary>
        /// initialize new instance
        /// </summary>
        /// <param name="parameterInfo">parameter information</param>
        /// <param name="method">method which declared this parameter</param>
        /// <param name="isRet">set to true for return parameter</param>
        public ParameterData(ParameterInfo parameterInfo, IMethodBase method, bool isRet = false)
        {
            DotNetMember = parameterInfo;
            DeclaringMember = method;
            ContentType = parameterInfo.ParameterType;
            IsReference = parameterInfo.ParameterType.IsByRef;
            Position = parameterInfo.Position;

            if (isRet)
            {
                Direction = ParameterDirection.ReturnValue;
                Position = -1;
                return;
            }

            if (parameterInfo.IsIn || !IsReference || !parameterInfo.IsOut)
                Direction = ParameterDirection.In;

            if (parameterInfo.IsOut)
                Direction |= ParameterDirection.Out;
        }

        /// <inheritdoc/>
        public new ParameterInfo DotNetMember { get; }

        /// <inheritdoc/>
        public new IMethodBase DeclaringMember { get; }
        
        /// <inheritdoc/>
        public Type ContentType { get; }
        
        /// <inheritdoc/>
        public bool IsReference { get; }
        
        /// <inheritdoc/>
        public ParameterDirection Direction { get; }
        
        /// <inheritdoc/>
        public int Position { get; }
    }
}
