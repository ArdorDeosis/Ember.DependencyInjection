

namespace Ember.DependencyInjection.ResolutionSources;

internal class InstanceContractSource<TContract> : IContractSource<TContract>, IDisposable where TContract : notnull
{
  private readonly TContract instance;

  public InstanceContractSource(TContract instance)
  {
    this.instance = instance;
  }

  public TContract Resolve(IActivator _) => instance;
  
  public void Dispose() => (instance as IDisposable)?.Dispose();
}