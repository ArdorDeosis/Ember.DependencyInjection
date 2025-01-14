using Ember.DependencyInjection.ResolutionSources;
using Ember.DependencyInjection.ResolutionStrategies;

namespace Ember.DependencyInjection.Configuration;

internal class ContractConfiguration<T> : IContractConfiguration<T>, IContractConfigurationWithSource, IDisposable where T : notnull
{
  private IContractSource<T> contractSource = new ConstructorContractSource<T, T>();
  private IContractResolutionStrategy<T> contractResolutionStrategy = new TransientContractResolution<T>();

  public Contract<T> BuildContract(IActivator activator) => new(activator, contractSource, contractResolutionStrategy);
  
  public IContractConfigurationWithSource To<TImplementation>() where TImplementation : T
  {
    contractSource = new ConstructorContractSource<T, TImplementation>();
    return this;
  }

  public IContractConfigurationWithSource ToMethod(Delegate method)
  {
    contractSource = new MethodContractSource<T>(method);
    return this;
  }

  public void ToInstance(T instance)
  {
    contractSource = new InstanceContractSource<T>(instance);
    AsSingleton();
  }

  public void AsSingleton() => contractResolutionStrategy = new SingletonContractResolution<T>();

  public void AsTransient() => contractResolutionStrategy = new TransientContractResolution<T>();

  public void Dispose()
  {
    (contractSource as IDisposable)?.Dispose();
    (contractResolutionStrategy as IDisposable)?.Dispose();
  }
}