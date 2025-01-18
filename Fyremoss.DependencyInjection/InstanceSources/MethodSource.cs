namespace Fyremoss.DependencyInjection;

/// <summary>
/// Produces a new instance from a factory method. Input parameters are resolved from the used <see cref="IInjector"/>.
/// </summary>
/// <inheritdoc />
internal class MethodSource<T> : IInstanceSource<T>
  where T : notnull
{
  private readonly Delegate factoryMethod;

  public MethodSource(Delegate factoryMethod)
  {
    if (factoryMethod.Method.ReturnType != typeof(T))
      throw new ArgumentException($"Delegate has wrong return type. Expected return type {typeof(T).FullName}.");
    this.factoryMethod = factoryMethod;
  }

  /// <inheritdoc />
  public T Resolve(IInjector injector) => injector.ExecuteMethod<T>(factoryMethod);
}