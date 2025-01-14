namespace Ember.DependencyInjection;

/// <summary>
/// Produces a new instance from the type's constructor.
/// </summary>
/// <inheritdoc />
internal class ConstructorContractSource<TContract, TImplementation> : IContractSource<TContract>
  where TImplementation : TContract where TContract : notnull
{
  /// <inheritdoc />
  public TContract Resolve(IActivator activator) => activator.CreateInstance<TImplementation>();
}