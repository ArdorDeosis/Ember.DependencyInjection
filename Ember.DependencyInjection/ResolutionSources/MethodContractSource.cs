

namespace Ember.DependencyInjection.ResolutionSources;

internal class MethodContractSource<TContract> : IContractSource<TContract>
  where TContract : notnull
{
  private readonly Delegate factoryMethod;
  
  public MethodContractSource(Delegate factoryMethod)
  {
    if (factoryMethod.Method.ReturnType != typeof(TContract))
      throw new Exception(); // TODO
    this.factoryMethod = factoryMethod;
  }
  public TContract Resolve(IActivator activator) => activator.ExecuteMethod<TContract>(factoryMethod);
}