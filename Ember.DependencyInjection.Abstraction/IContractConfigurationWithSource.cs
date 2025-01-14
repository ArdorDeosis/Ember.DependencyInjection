using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// Provides methods to configure the resolution strategy for a bound type.
/// </summary>
[PublicAPI]
public interface IContractConfigurationWithSource
{
  /// <summary>
  /// Configures the binding to use a singleton resolution strategy.
  /// </summary>
  void AsSingleton();
  
  /// <summary>
  /// Configures the binding to use a transient resolution strategy.
  /// </summary>
  void AsTransient();
}