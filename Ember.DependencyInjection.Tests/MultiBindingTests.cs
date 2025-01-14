using Ember.DependencyInjection.Tests.TestTypes;
using FluentAssertions;

namespace Ember.DependencyInjection.Tests;

public class MultiBindingTests
{
  [Test]
  public void MultiBinding_ResolveSingle_ResolvesFirst()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var activator = configuration.BuildContainer();
    
    // ACT
    var instance = activator.Resolve<IService>();
    
    // ASSERT
    instance.Should().BeOfType<ServiceA>();
  }
  
  // TODO: this could use a generic type case attribute
  [Test]
  public void MultiBinding_ResolveEnumerable_ResolvesAll()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var activator = configuration.BuildContainer();
    
    // ACT
    var instances = activator.Resolve<IEnumerable<IService>>().ToArray();
    
    // ASSERT
    instances.Should().HaveCount(2);
    instances.ElementAt(0).Should().BeOfType<ServiceA>();
    instances.ElementAt(1).Should().BeOfType<ServiceB>();
  }
  
  [Test]
  public void MultiBinding_ResolveReadOnlyCollection_ResolvesAll()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var activator = configuration.BuildContainer();
    
    // ACT
    var instances = activator.Resolve<IReadOnlyCollection<IService>>().ToArray();
    
    // ASSERT
    instances.Should().HaveCount(2);
    instances.ElementAt(0).Should().BeOfType<ServiceA>();
    instances.ElementAt(1).Should().BeOfType<ServiceB>();
  }
  
  [Test]
  public void MultiBinding_ResolveReadOnlyList_ResolvesAll()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    configuration.Bind<IService>().To<ServiceB>();
    var activator = configuration.BuildContainer();
    
    // ACT
    var instances = activator.Resolve<IReadOnlyList<IService>>().ToArray();
    
    // ASSERT
    instances.Should().HaveCount(2);
    instances.ElementAt(0).Should().BeOfType<ServiceA>();
    instances.ElementAt(1).Should().BeOfType<ServiceB>();
  }
  
  [Test]
  public void SingleBinding_ResolveEnumerable_ResolvesCollection()
  {
    // ARRANGE
    var configuration = new ContainerConfiguration();
    configuration.Bind<IService>().To<ServiceA>();
    var activator = configuration.BuildContainer();
    
    // ACT
    var instances = activator.Resolve<IReadOnlyList<IService>>().ToArray();
    
    // ASSERT
    instances.Should().HaveCount(1);
    instances.ElementAt(0).Should().BeOfType<ServiceA>();
  }
  
  // TODO: should this be here? should I test more failure cases in another class?
  [Test]
  public void NoBinding_ResolveEnumerable_Throws()
  {
    // ARRANGE
    var activator = new ContainerConfiguration().BuildContainer();
    
    // ACT
    var action = () => activator.Resolve<IEnumerable<IService>>();
    
    // ASSERT
    action.Should().Throw<DependencyResolutionException>();
  }
}