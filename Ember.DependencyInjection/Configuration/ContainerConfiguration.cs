namespace Ember.DependencyInjection;

/// <summary>
/// Configuration object for a dependency injection container.
/// </summary>
public class ContainerConfiguration : IContainerConfiguration
{
  private readonly ContractRegistry registry = new();

  /// <summary>
  /// Binds a contract to the container.
  /// </summary>
  /// <typeparam name="TContract">The type of the contract to bind.</typeparam>
  /// <returns>The configuration for the specified contract. Can be used for further configuration.</returns>
  /// <exception cref="ContainerConfigurationException">
  /// Thrown when the bind attempt fails.
  /// </exception>
  /// <remarks>
  /// <see cref="IActivator"/> cannot be bound manually, as it will be automatically bound when
  /// <see cref="BuildContainer"/> is called.
  /// </remarks>
  public IContractConfiguration<TContract> Bind<TContract>() where TContract : notnull
  {
    try
    {
      if (typeof(TContract) == typeof(IActivator))
        throw new ContainerConfigurationException($"Cannot bind a custom {nameof(IActivator)}. {nameof(IActivator)} will be bound to the created instance automatically when {nameof(BuildContainer)} is called.");
      return registry.Add<TContract>();
    }
    catch (Exception exception)
    {
      throw new ContainerConfigurationException("Failed to bind contract", exception);
    }
  }

  /// <summary>
  /// Builds the dependency injection container.
  /// </summary>
  /// <returns>An instance of <see cref="IActivator"/> that can be used to resolve dependencies.</returns>
  public IActivator BuildContainer() => new ServiceContainer(registry);
}