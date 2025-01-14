using Ember.DependencyInjection.ResolutionSources;

namespace Ember.DependencyInjection.ResolutionStrategies;

internal interface IContractResolutionStrategy<TContract> where TContract : notnull
{
  public TContract Resolve(IActivator activator, IContractSource<TContract> contractSource);
}