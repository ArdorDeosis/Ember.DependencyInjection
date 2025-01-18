namespace Fyremoss.DependencyInjection;

/// <summary>
/// Singleton instance caching strategy.
/// </summary>
/// <inheritdoc cref="IContractCachingStrategy{T}" />
internal class SingletonContractCaching<T> : IContractCachingStrategy<T>, IDisposable where T : notnull
{
  private readonly Lock lockObject = new();
  private volatile bool hasResolved;
  private T instance = default!;

  /// <inheritdoc />
  public T Resolve(IInjector injector, IInstanceSource<T> instanceSource)
  {
    if (!hasResolved)
    {
      lockObject.Enter();
      try
      {
        if (!hasResolved)
        {
          instance = instanceSource.Resolve(injector);
          hasResolved = true;
        }
      }
      finally
      {
        lockObject.Exit();
      }
    }

    return instance;
  }

  /// <inheritdoc />
  public void Dispose() => (instance as IDisposable)?.Dispose();
}