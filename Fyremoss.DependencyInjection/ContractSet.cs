using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace Fyremoss.DependencyInjection;

/// <summary>
/// A fixed set of contracts.
/// </summary>
internal class ContractSet
{
  private readonly FrozenDictionary<Type, IContract[]> contracts;

  internal ContractSet(FrozenDictionary<Type, IContract[]> contracts)
  {
    this.contracts = contracts;
  }

  /// <summary>
  /// Gets the list of contracts associated with the specified type.
  /// </summary>
  /// <param name="type">The type whose contracts are to be retrieved.</param>
  /// <returns>A list of contracts associated with the specified type.</returns>
  public IReadOnlyList<IContract> Get(Type type) => contracts.GetValueOrDefault(type, []);

  /// <summary>
  /// Tries to get the first contract associated with the specified type.
  /// </summary>
  /// <param name="type">The type whose first contract is to be retrieved.</param>
  /// <param name="contract">
  /// When this method returns, contains the first contract associated with the specified type, if found;
  /// otherwise, <c>null</c>.
  /// </param>
  /// <returns><c>true</c> if a contract is found; otherwise, <c>false</c>.</returns>
  public bool TryGetFirst(Type type, [NotNullWhen(true)] out IContract? contract)
  {
    contract = contracts.GetValueOrDefault(type, []).FirstOrDefault();
    return contract is not null;
  }
}