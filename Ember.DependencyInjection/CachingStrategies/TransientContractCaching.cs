namespace Ember.DependencyInjection;

/// <summary>
/// Transient caching strategy.
/// </summary>
/// <inheritdoc />
internal class TransientContractCaching<T> : IContractCachingStrategy<T> where T : notnull
{
  /// <inheritdoc />
  public T Resolve(IInjector injector, IInstanceSource<T> instanceSource) => instanceSource.Resolve(injector);
}