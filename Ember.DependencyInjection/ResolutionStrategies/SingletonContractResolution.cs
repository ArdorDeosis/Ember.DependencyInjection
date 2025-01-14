using Ember.DependencyInjection.ResolutionSources;

namespace Ember.DependencyInjection.ResolutionStrategies;

internal class SingletonContractResolution<TContract> : IContractResolutionStrategy<TContract>, IDisposable where TContract : notnull
{
  private readonly Lock lockObject = new();
  private volatile bool hasResolved;
  private TContract instance = default!;

  public TContract Resolve(IActivator activator, IContractSource<TContract> contractSource)
  {
    if (!hasResolved)
    {
      lockObject.Enter();
      try
      {
        if (!hasResolved)
        {
          instance = contractSource.Resolve(activator);
          hasResolved = true;
        }
      }
      finally
      {
        lockObject.Exit();
      }
    }

    return instance;
  }

  public void Dispose() => (instance as IDisposable)?.Dispose();
}