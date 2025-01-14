using System.Collections.Frozen;

namespace Ember.DependencyInjection.Configuration;

internal class ContractRegistry
{
  private readonly Dictionary<Type, List<object>> contracts = new();

  public IContractConfiguration<T> Add<T>() where T : notnull
  {
    var contractBuilder = new ContractConfiguration<T>();
    if (!contracts.ContainsKey(typeof(T)))
      contracts[typeof(T)] = [];
    contracts[typeof(T)].Add(contractBuilder);
    return contractBuilder;
  }

  public ContractSet MakeContractSet(IActivator activator) =>
    new(contracts.ToFrozenDictionary(
      entry => entry.Key,
      entry => entry.Value
        .Select(contractConfiguration => ((dynamic)contractConfiguration).BuildContract(activator))
        .Cast<IContract>()
        .ToArray()));
}