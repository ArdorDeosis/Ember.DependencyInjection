using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// Configuration object for a dependency injection container.
/// </summary>
[PublicAPI]
internal class ContainerConfiguration : IContainerConfiguration
{
  private readonly ContractRegistry registry = new();

  /// <inheritdoc />
  public ConstructorSelector ConstructorSelectionStrategy { private get; set; } = 
    ConstructorSelectors.WithAttribute.Then(ConstructorSelectors.MostParameters);

  /// <inheritdoc />
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

  /// <inheritdoc />
  public IActivator BuildContainer() => new ServiceContainer(registry, ConstructorSelectionStrategy);
}