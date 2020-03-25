using System;
using FluentAssertions;
using Modulr.Activation;
using Modulr.Tests.Common;
using Xunit;

namespace Modulr.Tests
{
    public class ModuleActivatorTests
    {
        [Fact]
        public void CanActivateModule()
        {
            var activator = new ModuleActivator<IDependencyModule>();

            var module = activator.Activate(typeof(TestDependencyModule));

            module.Should().NotBeNull();
            module.Should().BeOfType(typeof(TestDependencyModule));
        }

        [Fact]
        public void Activate_ShouldThrowOnInvalidType()
        {
            var activator = new ModuleActivator<IDependencyModule>();

            Action act = () => activator.Activate(typeof(ModuleActivatorTests));

            act.Should().Throw<ArgumentException>()
                .Where(argEx => argEx.ParamName == "moduleType");
        }
    }
}
