using Ember.DependencyInjection.Tests.TestTypes;
using FluentAssertions;

namespace Ember.DependencyInjection.Tests;

public class ConstructorResolutionTests
{
  [Test]
  public void CreateInstance_UsesImplicitParameterlessConstructor()
  {
    // ARRANGE
    var activator = new ContainerConfiguration().BuildContainer();
    
    // ACT
    var testObject = activator.CreateInstance<ConstructorTestObjectA>();

    // ASSERT
    testObject.UsedCorrectConstructor.Should().BeTrue();
  }
  
  [Test]
  public void CreateInstance_UsesExplicitConstructor()
  {
    // ARRANGE
    var activator = new ContainerConfiguration().BuildContainer();
    
    // ACT
    var testObject = activator.CreateInstance<ConstructorTestObjectB>();

    // ASSERT
    testObject.UsedCorrectConstructor.Should().BeTrue();
  }
  
  [Test]
  public void CreateInstance_PrefersConstructorWithMoreParameters()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<ServiceA>();
    var activator = configuration.BuildContainer();
    
    // ACT
    var testObject = activator.CreateInstance<ConstructorTestObjectC>();

    // ASSERT
    testObject.UsedCorrectConstructor.Should().BeTrue();
  }
  
  [Test]
  public void CreateInstance_IgnoresNonPublicConstructors()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<ServiceA>();
    configuration.Bind<ServiceB>();
    var activator = configuration.BuildContainer();
    
    // ACT
    var testObject = activator.CreateInstance<ConstructorTestObjectD>();

    // ASSERT
    testObject.UsedCorrectConstructor.Should().BeTrue();
  }
  
  [Test]
  public void CreateInstance_PrefersConstructorWithAttribute()
  {
    // ARRANGE
    var activator = new ContainerConfiguration().BuildContainer();
    
    // ACT
    var testObject = activator.CreateInstance<ConstructorTestObjectD>();

    // ASSERT
    testObject.UsedCorrectConstructor.Should().BeTrue();
  }
}