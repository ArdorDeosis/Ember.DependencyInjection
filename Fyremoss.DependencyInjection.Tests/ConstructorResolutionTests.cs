namespace Fyremoss.DependencyInjection.Tests;

public class ConstructorResolutionTests
{
  [Test]
  public void CreateInstance_UsesImplicitParameterlessConstructor()
  {
    // ARRANGE
    var injector = new InjectorConfiguration().BuildInjector();

    // ACT
    var testObject = injector.CreateInstance<ConstructorTestObjectA>();

    // ASSERT
    Assert.That(testObject.UsedCorrectConstructor, Is.True);
  }

  [Test]
  public void CreateInstance_UsesExplicitConstructor()
  {
    // ARRANGE
    var injector = new InjectorConfiguration().BuildInjector();

    // ACT
    var testObject = injector.CreateInstance<ConstructorTestObjectB>();

    // ASSERT
    Assert.That(testObject.UsedCorrectConstructor, Is.True);
  }

  [Test]
  public void CreateInstance_PrefersConstructorWithMoreParameters()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<ServiceA>();
    var injector = configuration.BuildInjector();

    // ACT
    var testObject = injector.CreateInstance<ConstructorTestObjectC>();

    // ASSERT
    Assert.That(testObject.UsedCorrectConstructor, Is.True);
  }

  [Test]
  public void CreateInstance_IgnoresNonPublicConstructors()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration();
    configuration.Bind<ServiceA>();
    configuration.Bind<ServiceB>();
    var injector = configuration.BuildInjector();

    // ACT
    var testObject = injector.CreateInstance<ConstructorTestObjectD>();

    // ASSERT
    Assert.That(testObject.UsedCorrectConstructor, Is.True);
  }

  [Test]
  public void CreateInstance_PrefersConstructorWithAttribute()
  {
    // ARRANGE
    var injector = new InjectorConfiguration().BuildInjector();

    // ACT
    var testObject = injector.CreateInstance<ConstructorTestObjectE>();

    // ASSERT
    Assert.That(testObject.UsedCorrectConstructor, Is.True);
  }

  [Test]
  public void CreateInstance_CustomConstructorSelection()
  {
    // ARRANGE
    var configuration = new InjectorConfiguration
    {
      // get parameterless ctor
      ConstructorSelectionStrategy = type =>
        type.GetConstructors().First(ctor => ctor.GetParameters().Length is 0),
    };
    var injector = configuration.BuildInjector();

    // ACT
    var testObject = injector.CreateInstance<ConstructorTestObjectF>();

    // ASSERT
    Assert.That(testObject.UsedCorrectConstructor, Is.True);
  }
}