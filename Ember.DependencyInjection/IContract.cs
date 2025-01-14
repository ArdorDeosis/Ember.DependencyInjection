namespace Ember.DependencyInjection;

/// <summary>
/// A contract for resolving instances of an unspecified type.
/// </summary>
internal interface IContract
{
  /// <summary>
  /// Resolves the contract.
  /// </summary>
  public object Resolve();
}