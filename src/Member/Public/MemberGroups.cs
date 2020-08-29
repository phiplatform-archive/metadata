using NoRealm.Phi.Metadata.Internal;
using System;
using System.Reflection;

namespace NoRealm.Phi.Metadata
{
    /// <summary>
    /// known member groups
    /// </summary>
    public static class MemberGroups
    {
        /// <summary>
        /// used by <see cref="IMemberCore"/> when there is no group for the member
        /// </summary>
        public static readonly IMemberGroup None =
            new MemberGroup(new Guid("c388f9ff99da4318a585e5e82e3e79fe"), MemberTypes.Custom);

        /// <summary>
        /// Member is a class
        /// </summary>
        public static readonly IMemberGroup Class =
            new MemberGroup(new Guid("28bfcac8e0e948b4b3c5c4f4b336e4df"), MemberTypes.TypeInfo);

        /// <summary>
        /// Member is a structure
        /// </summary>
        public static readonly IMemberGroup Structure =
            new MemberGroup(new Guid("29238e00d38b422db6109f699ebdcb06"), MemberTypes.TypeInfo);

        /// <summary>
        /// Member is a interface
        /// </summary>
        public static readonly IMemberGroup Interface =
            new MemberGroup(new Guid("7e8ba31a9be5494c8d6aa58370438e94"), MemberTypes.TypeInfo);

        /// <summary>
        /// Member is a field
        /// </summary>
        public static readonly IMemberGroup Field =
            new MemberGroup(new Guid("c640b0b7fd9e45748259ad8a22ce24f0"), MemberTypes.Field);

        /// <summary>
        /// Member is a constant
        /// </summary>
        public static readonly IMemberGroup Constant =
            new MemberGroup(new Guid("43570b894a124321b0a5f12565d34f58"), MemberTypes.Field);

        /// <summary>
        /// Member is a property
        /// </summary>
        public static readonly IMemberGroup Property =
            new MemberGroup(new Guid("44bc758e613a47b3a1f3a8418b30ef67"), MemberTypes.Property);

        /// <summary>
        /// Member is a constructor
        /// </summary>
        public static readonly IMemberGroup Constructor =
            new MemberGroup(new Guid("c3a8b0014acb48f1b159516f2a3b087b"), MemberTypes.Constructor);

        /// <summary>
        /// Member is a method
        /// </summary>
        public static readonly IMemberGroup Method =
            new MemberGroup(new Guid("acd11ee671b84bd6a29551b84760c5f1"), MemberTypes.Method);

        /// <summary>
        /// Member is a event
        /// </summary>
        public static readonly IMemberGroup Event =
            new MemberGroup(new Guid("e473656747d74820afe9e1a2b69ab6d0"), MemberTypes.Event);
    }
}
