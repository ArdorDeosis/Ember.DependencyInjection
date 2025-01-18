namespace Ember.DependencyInjection.Tests;

public class CreationHookTests
{
  [Test]
  public void ObjectIsCreated_CreationHooksAreExecuted()
  {
    // ARRANGE
    var hookExecuted = false;
    var configuration = new InjectorConfiguration();
    configuration.Bind<IService>().To<ServiceA>().AsTransient();
    configuration.AddCreationHook(_ => hookExecuted = true);
    var injector = configuration.BuildInjector();
    
    // ACT
    injector.Resolve<IService>();
    
    // ASSERT
    Assert.That(hookExecuted, Is.True);
  }
}