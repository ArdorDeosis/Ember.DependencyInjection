namespace Ember.DependencyInjection;

/// <summary>
/// Resolves to an existing instance.
/// </summary>
/// <inheritdoc cref="IInstanceSource{T}" />
internal class ReferenceSource<T> : IInstanceSource<T>, IDisposable where T : notnull
{
  private readonly T instance;

  public ReferenceSource(T instance)
  {
    this.instance = instance;
  }

  /// <inheritdoc />
  public T Resolve(IInjector _) => instance;

  /// <inheritdoc />
  public void Dispose() => (instance as IDisposable)?.Dispose();
}