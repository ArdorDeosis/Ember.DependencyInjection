using Ember.DependencyInjection.Tests.TestTypes;
using FluentAssertions;

namespace Ember.DependencyInjection.Tests;

public class CachingTests
{
  [Test]
  public void TransientCaching_CreatesNewInstances()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<ServiceA>().AsTransient();
    var activator = configuration.BuildContainer();

    // ACT
    var instance1 = activator.Resolve<ServiceA>();
    var instance2 = activator.Resolve<ServiceA>();
    
    // ASSERT
    instance1.Should().NotBe(instance2);
  }
  
  [Test]
  public void SingletonCaching_ReturnsSameInstance()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<ServiceA>().AsSingleton();
    var activator = configuration.BuildContainer();

    // ACT
    var instance1 = activator.Resolve<ServiceA>();
    var instance2 = activator.Resolve<ServiceA>();
    
    // ASSERT
    instance1.Should().Be(instance2);
  }
}