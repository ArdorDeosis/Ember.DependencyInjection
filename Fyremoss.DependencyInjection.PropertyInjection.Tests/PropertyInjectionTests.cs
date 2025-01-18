using Fyremoss.DependencyInjection.Tests;

namespace Fyremoss.DependencyInjection.PropertyInjection.Tests;

public class PropertyInjectionTests
{
  private static IEnumerable<TestCaseData> PropertyGetters()
  {
    yield return new TestCaseData((TestClass x) => x.PublicSet) { TestName = "public set" };
    yield return new TestCaseData((TestClass x) => x.PublicInit) { TestName = "public init" };
    yield return new TestCaseData((TestClass x) => x.PrivateSet) { TestName = "private set" };
    yield return new TestCaseData((TestClass x) => x.PrivateInit) { TestName = "private init" };
  }

  [TestCaseSource(nameof(PropertyGetters))]
  public void InjectProperties_DifferentSetters_AllGetInjected(Func<TestClass, IService> getter)
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    var service = new ServiceA();
    configuration.Bind<IService>().ToInstance(service);
    var injector = configuration.BuildInjector();

    // ACT
    var instance = new TestClass();
    injector.InjectProperties(instance);

    // ASSERT
    Assert.That(getter(instance), Is.EqualTo(service));
  }

  [Test]
  public void InjectProperties_SetterOnlyProperty_IsSet()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    var service = new ServiceA();
    configuration.Bind<IService>().ToInstance(service);
    var injector = configuration.BuildInjector();

    // ACT
    var instance = new TestClass();
    injector.InjectProperties(instance);

    // ASSERT
    Assert.That(instance.SetterOnlyHasBeenSet, Is.True);
  }

  [Test]
  public void InjectProperties_PropertyHasNoSetters_Throws()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    var service = new ServiceA();
    configuration.Bind<IService>().ToInstance(service);
    var injector = configuration.BuildInjector();

    // ACT
    var instance = new FailingTestClass();

    // ASSERT
    Assert.That(() => injector.InjectProperties(instance), Throws.TypeOf<DependencyResolutionException>());
  }
}