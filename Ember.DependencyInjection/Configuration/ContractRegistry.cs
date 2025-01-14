using System.Collections.Frozen;

namespace Ember.DependencyInjection;

/// <summary>
/// Manages the registration of contracts for dependency injection.
/// </summary>
internal class ContractRegistry
{
  private readonly Dictionary<Type, List<object>> contracts = new();

  /// <summary>
  /// Adds a new contract configuration for the specified type.
  /// </summary>
  /// <typeparam name="T">The type of the contract to add.</typeparam>
  /// <returns>The configuration for the specified contract for further configuration.</returns>
  public IContractConfiguration<T> Add<T>() where T : notnull
  {
    var contractBuilder = new ContractConfiguration<T>();
    if (!contracts.ContainsKey(typeof(T)))
      contracts[typeof(T)] = [];
    contracts[typeof(T)].Add(contractBuilder);
    return contractBuilder;
  }
  
  /// <summary>
  /// Creates a <see cref="ContractSet"/> from the registered contracts with the specified activator.
  /// </summary>
  /// <param name="activator">The activator used by the created contracts.</param>
  public ContractSet MakeContractSet(IActivator activator) =>
    new(contracts.ToFrozenDictionary(
      entry => entry.Key,
      entry => entry.Value
        .Select(contractConfiguration => ((dynamic)contractConfiguration).BuildContract(activator))
        .Cast<IContract>()
        .ToArray()));
}