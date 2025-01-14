

namespace Ember.DependencyInjection.ResolutionSources;

internal class ConstructorContractSource<TContract, TImplementation> : IContractSource<TContract>
  where TImplementation : TContract where TContract : notnull
{
  public TContract Resolve(IActivator activator) => activator.CreateInstance<TImplementation>();
}