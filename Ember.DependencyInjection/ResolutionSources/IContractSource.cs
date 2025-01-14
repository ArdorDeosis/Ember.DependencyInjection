

namespace Ember.DependencyInjection.ResolutionSources;

internal interface IContractSource<out TContract> where TContract : notnull
{
  public TContract Resolve(IActivator activator);
}