namespace Ember.DependencyInjection;

/// <summary>
/// Resolves to an existing instance.
/// </summary>
/// <inheritdoc cref="IContractSource{T}" />
internal class InstanceContractSource<T> : IContractSource<T>, IDisposable where T : notnull
{
  private readonly T instance;

  public InstanceContractSource(T instance)
  {
    this.instance = instance;
  }

  /// <inheritdoc />
  public T Resolve(IActivator _) => instance;

  /// <inheritdoc />
  public void Dispose() => (instance as IDisposable)?.Dispose();
}