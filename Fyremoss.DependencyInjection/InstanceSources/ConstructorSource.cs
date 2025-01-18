namespace Fyremoss.DependencyInjection;

/// <summary>
/// Produces a new instance from the type's constructor.
/// </summary>
/// <inheritdoc />
internal class ConstructorSource<TContract, TImplementation> : IInstanceSource<TContract>
  where TImplementation : TContract where TContract : notnull
{
  /// <inheritdoc />
  public TContract Resolve(IInjector injector) => injector.CreateInstance<TImplementation>();
}