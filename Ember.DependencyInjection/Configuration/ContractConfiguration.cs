namespace Ember.DependencyInjection;

/// <summary>
/// Configuration for a contract in the dependency injection container.
/// </summary>
/// <typeparam name="T">The type of the contract.</typeparam>
internal class ContractConfiguration<T> : IContractConfiguration<T>, IContractConfigurationWithSource, IDisposable where T : notnull
{
  private IContractSource<T> contractSource = new ConstructorContractSource<T, T>();
  private IContractCachingStrategy<T> contractCachingStrategy = new TransientContractCaching<T>();

  /// <summary>
  /// Builds the contract with the specified activator.
  /// </summary>
  /// <param name="activator">The activator used by the created contract instances.</param>
  public Contract<T> BuildContract(IActivator activator) => new(activator, contractSource, contractCachingStrategy);

  /// <inheritdoc />
  public IContractConfigurationWithSource To<TImplementation>() where TImplementation : T
  {
    contractSource = new ConstructorContractSource<T, TImplementation>();
    return this;
  }

  /// <inheritdoc />
  public IContractConfigurationWithSource ToMethod(Delegate method)
  {
    contractSource = new MethodContractSource<T>(method);
    return this;
  }

  /// <inheritdoc />
  public void ToInstance(T instance)
  {
    contractSource = new InstanceContractSource<T>(instance);
    AsSingleton();
  }

  /// <inheritdoc cref="IContractConfiguration{TContract}.AsSingleton" />
  public void AsSingleton() => contractCachingStrategy = new SingletonContractCaching<T>();

  /// <inheritdoc cref="IContractConfiguration{TContract}.AsTransient" />
  public void AsTransient() => contractCachingStrategy = new TransientContractCaching<T>();

  /// <inheritdoc />
  public void Dispose()
  {
    (contractSource as IDisposable)?.Dispose();
    (contractCachingStrategy as IDisposable)?.Dispose();
  }
}