namespace Ember.DependencyInjection;

/// <summary>
/// A contract for resolving instances of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the instance to resolve. Must be a non-nullable type.</typeparam>
internal class Contract<T> : IContract, IDisposable where T : notnull
{
  private readonly IActivator activator;
  private readonly IContractSource<T> contractSource;
  private readonly IContractCachingStrategy<T> contractCachingStrategy;

  public Contract(IActivator activator, IContractSource<T> contractSource, IContractCachingStrategy<T> contractCachingStrategy)
  {
    this.activator = activator;
    this.contractSource = contractSource;
    this.contractCachingStrategy = contractCachingStrategy;
  }

  /// <summary>
  /// Resolves the contract.
  /// </summary>
  public T Resolve() => contractCachingStrategy.Resolve(activator, contractSource);

  /// <inheritdoc />
  object IContract.Resolve() => Resolve();

  /// <inheritdoc />
  public void Dispose()
  {
    (contractSource as IDisposable)?.Dispose();
    (contractCachingStrategy as IDisposable)?.Dispose();
  }
}