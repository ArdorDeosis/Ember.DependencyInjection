namespace Ember.DependencyInjection;

/// <summary>
/// Configuration object for an injector.
/// </summary>
public class InjectorConfiguration : IInjectorConfiguration
{
  private readonly ContractRegistry registry = new();
  private readonly HashSet<CreationHook> hooks = [];

  /// <inheritdoc />
  public ConstructorSelector ConstructorSelectionStrategy { private get; set; } = 
    ConstructorSelectors.WithAttribute.Then(ConstructorSelectors.MostParameters);

  /// <inheritdoc />
  /// <remarks>
  /// <see cref="IInjector"/> cannot be bound manually, as it will be automatically bound when
  /// <see cref="BuildInjector"/> is called.
  /// </remarks>
  public IContractConfiguration<TContract> Bind<TContract>() where TContract : notnull
  {
    try
    {
      if (typeof(TContract) == typeof(IInjector))
        throw new InjectorConfigurationException($"Cannot bind a custom {nameof(IInjector)}. {nameof(IInjector)} will be bound to the created instance automatically when {nameof(BuildInjector)} is called.");
      return registry.Add<TContract>();
    }
    catch (Exception exception)
    {
      throw new InjectorConfigurationException("Failed to bind contract", exception);
    }
  }

  /// <inheritdoc />
  public void AddCreationHook(CreationHook creationHook) => hooks.Add(creationHook);

  /// <inheritdoc />
  public IInjector BuildInjector() => new Injector(registry, ConstructorSelectionStrategy, new HashSet<CreationHook>(hooks));
}