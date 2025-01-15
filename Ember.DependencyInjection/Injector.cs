using System.Reflection;

namespace Ember.DependencyInjection;

/// <inheritdoc />
internal class Injector : IInjector
{
  private readonly DependencyResolver resolver;
  private readonly CachedConstructorSelector constructorSelector;

  internal Injector(ContractRegistry registry, ConstructorSelector constructorSelector)
  {
    registry.Add<IInjector>().ToInstance(this);
    resolver = new DependencyResolver(registry.MakeContractSet(this));
    this.constructorSelector = new CachedConstructorSelector(constructorSelector);
  }

  /// <inheritdoc />
  public T CreateInstance<T>()
  {
    if (constructorSelector.GetConstructor(typeof(T)) is not {} constructor)
      throw new ArgumentException($"Cannot create an instance of type {typeof(T).FullName}; the type has no public constructor.");

    try
    {
      var parameters = ResolveParameterList(constructor);
      return (T)constructor.Invoke(parameters);
    }
    catch (Exception exception) when (exception is not DependencyResolutionException)
    {
      throw new DependencyResolutionException(
        $"Failed to create instance of type {typeof(T).FullName}. See inner exception for more information", exception);
    }
  }

  /// <inheritdoc />
  public T ExecuteMethod<T>(Delegate factoryMethod)
  {
    ArgumentNullException.ThrowIfNull(factoryMethod);
    if (factoryMethod.Method.ReturnType != typeof(T))
      throw new ArgumentException($"Delegate has wrong return type. Expected return type {typeof(T).FullName}.");

    try
    {
      var parameters = ResolveParameterList(factoryMethod.Method);
      return (T)factoryMethod.DynamicInvoke(parameters)!;
    }
    catch (Exception exception) when (exception is not DependencyResolutionException)
    {
      throw new DependencyResolutionException(
        $"Failed to create instance of type {typeof(T).FullName}. See inner exception for more information", exception);
    }
  }

  /// <inheritdoc />
  public T Resolve<T>()
  {
    try
    {
      var instance = resolver.Resolve(typeof(T));
      if (instance.GetType().IsAssignableTo(typeof(T)))
        return (T)instance;
      throw new DependencyResolutionException(
        $"Resolved instance is not assignable to resolved type {typeof(T).FullName}");
    }
    catch (Exception exception) when (exception is not DependencyResolutionException)
    {
      throw new DependencyResolutionException(
        $"Failed to create instance of type {typeof(T).FullName}. See inner exception for more information", exception);
    }
  }

  private object[] ResolveParameterList(MethodBase method) =>
    method.GetParameters().Select(parameter => resolver.Resolve(parameter.ParameterType)).ToArray();
}