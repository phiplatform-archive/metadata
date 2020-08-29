using System;
using System.Collections.Generic;

namespace NoRealm.Phi.Metadata.Activator.Internal
{
    /// <summary>
    /// represent equality comparer for activation key
    /// </summary>
    internal class ActivatorKeyEqualityComparer : IEqualityComparer<ActivatorKey>
    {
        public bool Equals(ActivatorKey x, ActivatorKey y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            if (x.GetHashCode() != y.GetHashCode()) return false;
            if (x.Owner != y.Owner) return false;
            if (x.ParamTypes.Count != y.ParamTypes.Count) return false;

            for (var i = 0; i < x.ParamTypes.Count; ++i)
            {
                if (x.ParamTypes[i] != y.ParamTypes[i]) return false;
            }

            return true;
        }

        public int GetHashCode(ActivatorKey obj) => obj.GetHashCode();
    }
}
