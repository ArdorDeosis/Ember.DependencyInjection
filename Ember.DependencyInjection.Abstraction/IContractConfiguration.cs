using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// A type contract for the injector.
/// </summary>
/// <typeparam name="TContract">The type bound in the injector.</typeparam>
/// <remarks>
/// By default, the bound type is bound to itself with a transient resolution strategy.
/// To change this, use the fluent setup interface on this type.
/// </remarks>
[PublicAPI]
public interface IContractConfiguration<in TContract>
{
  /// <summary>
  /// Binds the contract type to the specified implementation type.
  /// </summary>
  /// <typeparam name="TImplementation">The implementation type that satisfies the contract.</typeparam>
  /// <returns>An interface to further configure the contract.</returns>
  IContractConfigurationWithSource To<TImplementation>() where TImplementation : notnull, TContract;
  
  /// <summary>
  /// Binds the contract type to the specified instance as singleton.
  /// </summary>
  /// <param name="instance">The instance that satisfies the contract.</param>
  void ToInstance(TContract instance);
  
  /// <summary>
  /// Binds the contract type to a method that produces an instance.
  /// </summary>
  /// <param name="method">The method that returns an instance of the contract type.</param>
  /// <returns>An interface to further configure the contract.</returns>
  IContractConfigurationWithSource ToMethod(Delegate method);

  /// <summary>
  /// Configures the binding to use a singleton resolution strategy.
  /// </summary>
  void AsSingleton();
  
  /// <summary>
  /// Configures the binding to use a transient resolution strategy.
  /// </summary>
  void AsTransient();
}