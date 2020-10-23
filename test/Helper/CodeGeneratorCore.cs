using System;
using System.Reflection;
using NoRealm.Phi.Metadata.Activator.Internal;
using NoRealm.Phi.Metadata.CodeGeneration;
using NoRealm.Phi.Metadata.Test.Data;
using Xunit;

namespace NoRealm.Phi.Metadata.Test.Fixture
{
    public abstract class CodeGeneratorCore
    {
        private readonly ICodeGenerator generator;
        

        internal CodeGeneratorCore(ICodeGenerator generator)
        {
            this.generator = generator;
        }

        [Fact]
        public void TestConstructorNoArgs()
        {
            var ctor = User.Constructors.Get();
            var key = new ActivatorKey(User.Type, ctor);
            var fn = generator.CreateActivator(key, ctor);

            var obj = fn(null) as User;
            Assert.NotNull(obj);
        }

        [Fact]
        public void TestConstructorOneArg()
        {
            var ctor = User.Constructors.Get<Guid>();
            var key = new ActivatorKey(User.Type, ctor);
            var fn = generator.CreateActivator(key, ctor);

            var userId = Guid.NewGuid();

            var obj = fn(new object[]{userId}) as User;
            Assert.NotNull(obj);
            Assert.Equal(userId, obj.UserId);
        }

        [Fact]
        public void TestConstructorTwoArgs()
        {
            var ctor = User.Constructors.Get<Guid, string>();
            var key = new ActivatorKey(User.Type, ctor);
            var fn = generator.CreateActivator(key, ctor);

            var userId = Guid.NewGuid();
            var name = "test";

            var obj = fn(new object[]{userId, name}) as User;
            Assert.NotNull(obj);
            Assert.Equal(userId, obj.UserId);
            Assert.Equal(name, obj.Name);
        }

        [Fact]
        public void TestConstructorThreeArgs()
        {
            var ctor = User.Constructors.Get<Guid, string, bool>();
            var key = new ActivatorKey(User.Type, ctor);
            var fn = generator.CreateActivator(key, ctor);

            var userId = Guid.NewGuid();
            var name = "test";
            var isActive = true;

            var obj = fn(new object[]{userId, name, isActive}) as User;
            Assert.NotNull(obj);
            Assert.Equal(userId, obj.UserId);
            Assert.Equal(name, obj.Name);
            Assert.Equal(isActive, obj.isActive);
        }
    }
}
