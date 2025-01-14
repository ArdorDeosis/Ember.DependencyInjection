using Ember.DependencyInjection.ResolutionSources;
using Ember.DependencyInjection.ResolutionStrategies;

namespace Ember.DependencyInjection;

internal class Contract<T> : IContract, IDisposable where T : notnull
{
  private readonly IActivator activator;
  private readonly IContractSource<T> contractSource;
  private readonly IContractResolutionStrategy<T> contractResolutionStrategy;

  public Contract(IActivator activator, IContractSource<T> contractSource, IContractResolutionStrategy<T> contractResolutionStrategy)
  {
    this.activator = activator;
    this.contractSource = contractSource;
    this.contractResolutionStrategy = contractResolutionStrategy;
  }

  public T Resolve() => contractResolutionStrategy.Resolve(activator, contractSource);
  object IContract.Resolve() => Resolve();

  public void Dispose()
  {
    (contractSource as IDisposable)?.Dispose();
    (contractResolutionStrategy as IDisposable)?.Dispose();
  }
}