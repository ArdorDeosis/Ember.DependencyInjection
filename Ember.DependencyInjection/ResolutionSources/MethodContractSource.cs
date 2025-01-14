namespace Ember.DependencyInjection;

/// <summary>
/// Produces a new instance from a factory method. Input parameters are resolved from the used <see cref="IActivator"/>.
/// </summary>
/// <inheritdoc />
internal class MethodContractSource<T> : IContractSource<T>
  where T : notnull
{
  private readonly Delegate factoryMethod;

  public MethodContractSource(Delegate factoryMethod)
  {
    if (factoryMethod.Method.ReturnType != typeof(T))
      throw new ArgumentException($"Delegate has wrong return type. Expected return type {typeof(T).FullName}.");
    this.factoryMethod = factoryMethod;
  }

  /// <inheritdoc />
  public T Resolve(IActivator activator) => activator.ExecuteMethod<T>(factoryMethod);
}