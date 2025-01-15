using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// An error that occurs during the configuration of the dependency injection container.
/// </summary>
[PublicAPI]
public class ContainerConfigurationException : Exception
{
  /// <inheritdoc />
  public ContainerConfigurationException(string message) : base(message) { }

  /// <inheritdoc />
  public ContainerConfigurationException(string message, Exception innerException) : base(message, innerException) { }
}