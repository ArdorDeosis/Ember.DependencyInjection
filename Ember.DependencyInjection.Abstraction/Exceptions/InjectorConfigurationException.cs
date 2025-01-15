using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// An error that occurs during the configuration of the injector.
/// </summary>
[PublicAPI]
public class InjectorConfigurationException : Exception
{
  /// <inheritdoc />
  public InjectorConfigurationException(string message) : base(message) { }

  /// <inheritdoc />
  public InjectorConfigurationException(string message, Exception innerException) : base(message, innerException) { }
}