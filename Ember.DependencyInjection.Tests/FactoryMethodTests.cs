using Ember.DependencyInjection.Tests.TestTypes;
using FluentAssertions;

namespace Ember.DependencyInjection.Tests;

public class FactoryMethodTests
{
  [Test]
  public void FactoryMethod_IsUsed()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<FactoryMethodTestObject>().ToMethod(() => new FactoryMethodTestObject { ManualFlag = true });
    var activator = configuration.BuildContainer();

    // ACT
    var instance = activator.Resolve<FactoryMethodTestObject>();

    // ASSERT
    instance.ManualFlag.Should().BeTrue();
  }
  
  [Test]
  public void FactoryMethod_ParametersAreResolved()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    var serviceInstance = new ServiceA();
    configuration.Bind<IService>().ToInstance(serviceInstance);
    configuration.Bind<FactoryMethodTestObject>().ToMethod((IService service) => 
      new FactoryMethodTestObject { ManualFlag = service == serviceInstance });
    var activator = configuration.BuildContainer();

    // ACT
    var instance = activator.Resolve<FactoryMethodTestObject>();

    // ASSERT
    instance.ManualFlag.Should().BeTrue();
  }
}