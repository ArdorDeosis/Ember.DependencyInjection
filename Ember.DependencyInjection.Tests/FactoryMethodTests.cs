using Ember.DependencyInjection.Tests.TestTypes;

namespace Ember.DependencyInjection.Tests;

public class FactoryMethodTests
{
  [Test]
  public void FactoryMethod_IsUsed()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<FactoryMethodTestObject>().ToMethod(() => new FactoryMethodTestObject { ManualFlag = true });
    var injector = configuration.BuildInjector();

    // ACT
    var instance = injector.Resolve<FactoryMethodTestObject>();

    // ASSERT
    Assert.That(instance.ManualFlag, Is.True);
  }

  [Test]
  public void FactoryMethod_ParametersAreResolved()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    var serviceInstance = new ServiceA();
    configuration.Bind<IService>().ToInstance(serviceInstance);
    configuration.Bind<FactoryMethodTestObject>().ToMethod((IService service) =>
      new FactoryMethodTestObject { ManualFlag = service == serviceInstance });
    var injector = configuration.BuildInjector();

    // ACT
    var instance = injector.Resolve<FactoryMethodTestObject>();

    // ASSERT
    Assert.That(instance.ManualFlag, Is.True);
  }
}