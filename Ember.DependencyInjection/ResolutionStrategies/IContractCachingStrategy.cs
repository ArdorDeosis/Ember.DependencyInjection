namespace Ember.DependencyInjection;

/// <summary>
/// A strategy for caching resolved instances. (e.g. Transient, Singleton, ...)
/// </summary>
/// <typeparam name="T">The type of the instance.</typeparam>
internal interface IContractCachingStrategy<T> where T : notnull
{
  /// <summary>
  /// Resolves an instance of type <typeparamref name="T"/>.
  /// </summary>
  /// <param name="activator">The activator used to resolve the instances.</param>
  /// <param name="contractSource">The source producing the instance.</param>
  T Resolve(IActivator activator, IContractSource<T> contractSource);
}