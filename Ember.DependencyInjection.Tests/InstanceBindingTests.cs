using Ember.DependencyInjection.Tests.TestTypes;

namespace Ember.DependencyInjection.Tests;

public class InstanceBindingTests
{
  [Test]
  public void InstanceBinding_ResolvesInstance()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    var instance = new ServiceA();
    configuration.Bind<IService>().ToInstance(instance);
    var injector = configuration.BuildInjector();

    // ACT
    var resolvedInstance = injector.Resolve<IService>();

    // ASSERT
    Assert.That(resolvedInstance, Is.EqualTo(instance));
  }
}