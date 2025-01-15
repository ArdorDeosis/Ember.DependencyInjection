namespace Ember.DependencyInjection;

/// <summary>
/// Configuration for a contract in the dependency injection container.
/// </summary>
/// <typeparam name="T">The type of the contract.</typeparam>
internal class ContractConfiguration<T> : IContractConfiguration<T>, IContractConfigurationWithSource, IDisposable where T : notnull
{
  private IInstanceSource<T> instanceSource = new ConstructorSource<T, T>();
  private IContractCachingStrategy<T> contractCachingStrategy = new TransientContractCaching<T>();

  /// <summary>
  /// Builds the contract with the specified activator.
  /// </summary>
  /// <param name="activator">The activator used by the created contract instances.</param>
  public Contract<T> BuildContract(IActivator activator) => new(activator, instanceSource, contractCachingStrategy);

  /// <inheritdoc />
  public IContractConfigurationWithSource To<TImplementation>() where TImplementation : T
  {
    instanceSource = new ConstructorSource<T, TImplementation>();
    return this;
  }

  /// <inheritdoc />
  public IContractConfigurationWithSource ToMethod(Delegate method)
  {
    instanceSource = new MethodSource<T>(method);
    return this;
  }

  /// <inheritdoc />
  public void ToInstance(T instance)
  {
    instanceSource = new ReferenceSource<T>(instance);
    AsSingleton();
  }

  /// <inheritdoc cref="IContractConfiguration{TContract}.AsSingleton" />
  public void AsSingleton() => contractCachingStrategy = new SingletonContractCaching<T>();

  /// <inheritdoc cref="IContractConfiguration{TContract}.AsTransient" />
  public void AsTransient() => contractCachingStrategy = new TransientContractCaching<T>();

  /// <inheritdoc />
  public void Dispose()
  {
    (instanceSource as IDisposable)?.Dispose();
    (contractCachingStrategy as IDisposable)?.Dispose();
  }
}