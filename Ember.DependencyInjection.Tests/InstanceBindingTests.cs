using Ember.DependencyInjection.Tests.TestTypes;
using FluentAssertions;

namespace Ember.DependencyInjection.Tests;

public class InstanceBindingTests
{
  [Test]
  public void InstanceBinding_ResolvesInstance()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    var instance = new ServiceA();
    configuration.Bind<IService>().ToInstance(instance);
    var activator = configuration.BuildContainer();
    
    // ACT
    var resolvedInstance = activator.Resolve<IService>();
    
    // ASSERT
    resolvedInstance.Should().Be(instance);
  }
}