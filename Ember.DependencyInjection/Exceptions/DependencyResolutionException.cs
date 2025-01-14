namespace Ember.DependencyInjection;

/// <summary>
/// An error that occurs during the resolution of dependencies.
/// </summary>
public class DependencyResolutionException : Exception
{
  /// <inheritdoc />
  public DependencyResolutionException(string message) : base(message) { }

  /// <inheritdoc />
  public DependencyResolutionException(string message, Exception innerException) : base(message, innerException) { }
}