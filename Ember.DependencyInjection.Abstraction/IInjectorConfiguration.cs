using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// Provides methods for configuring an injector.
/// </summary>
[PublicAPI]
public interface IInjectorConfiguration
{
  /// <summary>
  /// Sets the constructor selection strategy.
  /// </summary>
  ConstructorSelector ConstructorSelectionStrategy { set; }

  /// <summary>
  /// Begins the binding process for the specified contract type.
  /// </summary>
  /// <typeparam name="TContract">The type to be bound in the injector.</typeparam>
  /// <returns>An interface to further configure the contract binding.</returns>
  IContractConfiguration<TContract> Bind<TContract>() where TContract : notnull;

  /// <summary>
  /// Builds the injector from this configuration.
  /// </summary>
  /// <returns>An instance of <see cref="IInjector"/> that can be used to resolve dependencies.</returns>
  IInjector BuildInjector();
}