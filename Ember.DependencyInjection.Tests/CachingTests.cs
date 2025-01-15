using Ember.DependencyInjection.Tests.TestTypes;
using FluentAssertions;

namespace Ember.DependencyInjection.Tests;

public class CachingTests
{
  [Test]
  public void TransientCaching_CreatesNewInstances()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<ServiceA>().AsTransient();
    var injector = configuration.BuildInjector();

    // ACT
    var instance1 = injector.Resolve<ServiceA>();
    var instance2 = injector.Resolve<ServiceA>();
    
    // ASSERT
    instance1.Should().NotBe(instance2);
  }
  
  [Test]
  public void SingletonCaching_ReturnsSameInstance()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<ServiceA>().AsSingleton();
    var injector = configuration.BuildInjector();

    // ACT
    var instance1 = injector.Resolve<ServiceA>();
    var instance2 = injector.Resolve<ServiceA>();
    
    // ASSERT
    instance1.Should().Be(instance2);
  }
}