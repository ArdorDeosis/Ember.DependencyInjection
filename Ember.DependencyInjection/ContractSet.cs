using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace Ember.DependencyInjection;

internal class ContractSet
{
  private readonly FrozenDictionary<Type, IContract[]> contracts;
  
  internal ContractSet(FrozenDictionary<Type, IContract[]> contracts)
  {
    this.contracts = contracts;
  }

  public IReadOnlyList<IContract> Get(Type type) => contracts.GetValueOrDefault(type, []);
  
  public bool TryGetFirst(Type type, [NotNullWhen(true)] out IContract? contract)
  {
    contract = contracts.GetValueOrDefault(type, []).FirstOrDefault();
    return contract is not null;
  }
}