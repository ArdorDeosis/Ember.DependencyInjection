using Ember.DependencyInjection.Exceptions;

namespace Ember.DependencyInjection.Configuration;

public class ContainerConfiguration : IContainerConfiguration
{
  private readonly ContractRegistry registry = new();

  public IContractConfiguration<TContract> Bind<TContract>() where TContract : notnull
  {
    if (typeof(TContract) == typeof(IActivator))
      throw new ContainerConfigurationException("TODO");
    return registry.Add<TContract>();
  }

  public ServiceContainer BuildContainer() => new(registry);
}