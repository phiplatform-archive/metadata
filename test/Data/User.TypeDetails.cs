using System;
using System.Linq;
using System.Reflection;
using static NoRealm.Phi.Metadata.Test.ReflectionExtensions;

namespace NoRealm.Phi.Metadata.Test.Data
{
    public partial class User
    {
        public static readonly Type Type = typeof(User);

        public static class Properties
        {
            public static readonly PropertyInfo UserId = GetProperty<User>(e => e.UserId);
            public static readonly PropertyInfo Name = GetProperty<User>(e => e.Name);
            public static readonly PropertyInfo BirthDate = GetProperty<User>(e => e.BirthDate);
            public static readonly PropertyInfo IsActive = GetProperty<User>(e => e.IsActive);
            public static readonly PropertyInfo WriteOnly = GetProperty<User>(nameof(User.WriteOnly));
            public static readonly PropertyInfo ReadOnly = GetProperty<User>(e => e.ReadOnly);
        }

        public static class Fields
        {
            public static readonly FieldInfo MaxUsersConst = GetField<User>(nameof(User.MaxUsers));

            public static readonly FieldInfo SomeValue = GetField<User>(e => e.SomeValue);
            public static readonly FieldInfo UserId = GetField<User>(e => e.userId);
            public static readonly FieldInfo Name = GetField<User>(e => e.name);
            public static readonly FieldInfo IsActive = GetField<User>(e => e.isActive);
            public static readonly FieldInfo Dob = GetField<User>(e => e.dob);
        }

        public static class Methods
        {
            //public static MethodInfo Get<T>(string name)
            //    => Get(name, typeof(T));

            //public static MethodInfo Get<T1, T2>(string name)
            //    => Get(name, typeof(T1), typeof(T2));

            //public static MethodInfo Get<T1, T2, T3>(string name)
            //    => Get(name, typeof(T1), typeof(T2), typeof(T3));

            //public static MethodInfo Get<T1, T2, T3, T4>(string name)
            //    => Get(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4));

            //public static MethodInfo Get<T1, T2, T3, T4, T5>(string name)
            //    => Get(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

            //public static MethodInfo Get<T1, T2, T3, T4, T5, T6>(string name)
            //    => Get(name, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        }

        public static class Constructors
        {
            public static ConstructorInfo Get()
                => Get(Type.EmptyTypes);

            public static ConstructorInfo Get<T>()
                => Get(typeof(T));

            public static ConstructorInfo Get<T1, T2>()
                => Get(typeof(T1), typeof(T2));

            public static ConstructorInfo Get<T1, T2, T3>()
                => Get(typeof(T1), typeof(T2), typeof(T3));
                
            public static ConstructorInfo Get<T1, T2, T3, T4>()
                => Get(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

            public static ConstructorInfo Get<T1, T2, T3, T4, T5>()
                => Get(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

            public static ConstructorInfo Get<T1, T2, T3, T4, T5, T6>()
                => Get(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));

            private static ConstructorInfo Get(params Type[] types)
                => Type.GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                    null, types, null);
        }

    }
}
