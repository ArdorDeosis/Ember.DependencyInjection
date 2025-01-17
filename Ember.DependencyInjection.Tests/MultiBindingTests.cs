namespace Ember.DependencyInjection.Tests;

public class MultiBindingTests
{
  [Test]
  public void MultiBinding_ResolveSingle_ResolvesFirst()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var injector = configuration.BuildInjector();

    // ACT
    var instance = injector.Resolve<IService>();

    // ASSERT
    Assert.That(instance, Is.TypeOf<ServiceA>());
  }

  // TODO: this could use a generic type case attribute
  [Test]
  public void MultiBinding_ResolveEnumerable_ResolvesAll()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var injector = configuration.BuildInjector();

    // ACT
    var instances = injector.Resolve<IEnumerable<IService>>().ToArray();

    // ASSERT
    Assert.That(instances, Has.Length.EqualTo(2));
    Assert.That(instances[0], Is.TypeOf<ServiceA>());
    Assert.That(instances[1], Is.TypeOf<ServiceB>());
  }

  [Test]
  public void MultiBinding_ResolveReadOnlyCollection_ResolvesAll()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var injector = configuration.BuildInjector();

    // ACT
    var instances = injector.Resolve<IReadOnlyCollection<IService>>().ToArray();

    // ASSERT
    Assert.That(instances, Has.Length.EqualTo(2));
    Assert.That(instances[0], Is.TypeOf<ServiceA>());
    Assert.That(instances[1], Is.TypeOf<ServiceB>());
  }

  [Test]
  public void MultiBinding_ResolveReadOnlyList_ResolvesAll()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var injector = configuration.BuildInjector();

    // ACT
    var instances = injector.Resolve<IReadOnlyList<IService>>().ToArray();

    // ASSERT
    Assert.That(instances, Has.Length.EqualTo(2));
    Assert.That(instances[0], Is.TypeOf<ServiceA>());
    Assert.That(instances[1], Is.TypeOf<ServiceB>());
  }

  [Test]
  public void SingleBinding_ResolveEnumerable_ResolvesCollection()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    var injector = configuration.BuildInjector();

    // ACT
    var instances = injector.Resolve<IReadOnlyList<IService>>().ToArray();

    // ASSERT
    Assert.That(instances, Has.Length.EqualTo(1));
    Assert.That(instances[0], Is.TypeOf<ServiceA>());
  }

  // TODO: should this be here? should I test more failure cases in another class?
  [Test]
  public void NoBinding_ResolveEnumerable_Throws()
  {
    // ARRANGE
    var injector = new InjectorConfiguration().BuildInjector();

    // ACT
    var action = () => injector.Resolve<IEnumerable<IService>>();

    // ASSERT
    Assert.That(action, Throws.TypeOf<DependencyResolutionException>());
  }
}