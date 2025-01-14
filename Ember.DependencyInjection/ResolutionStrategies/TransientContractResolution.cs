using Ember.DependencyInjection.ResolutionSources;

namespace Ember.DependencyInjection.ResolutionStrategies;

internal class TransientContractResolution<TContract> : IContractResolutionStrategy<TContract> where TContract : notnull
{
  public TContract Resolve(IActivator activator, IContractSource<TContract> contractSource) => contractSource.Resolve(activator);
}