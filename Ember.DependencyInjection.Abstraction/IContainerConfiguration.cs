using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// Provides methods for configuring the DI container.
/// </summary>
[PublicAPI]
public interface IContainerConfiguration
{
  /// <summary>
  /// Sets the constructor selection strategy.
  /// </summary>
  ConstructorSelector ConstructorSelectionStrategy { set; }

  /// <summary>
  /// Begins the binding process for the specified contract type.
  /// </summary>
  /// <typeparam name="TContract">The type to be bound in the container.</typeparam>
  /// <returns>An interface to further configure the contract binding.</returns>
  IContractConfiguration<TContract> Bind<TContract>() where TContract : notnull;
}