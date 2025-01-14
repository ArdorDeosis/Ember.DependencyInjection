using System.Reflection;
using Ember.DependencyInjection.Configuration;
using Ember.DependencyInjection.Exceptions;

namespace Ember.DependencyInjection;

public class ServiceContainer : IActivator
{
  private readonly DependencyResolver resolver;

  internal ServiceContainer(ContractRegistry registry)
  {
    registry.Add<IActivator>().ToInstance(this);
    resolver = new DependencyResolver(registry.MakeContractSet(this));
  }

  public T CreateInstance<T>()
  {
    // TODO: enhance constructor selection
    if (typeof(T).GetConstructors() is not { Length: > 0 } constructors)
      throw new DependencyResolutionException($"Cannot create an instance of type {typeof(T).FullName}");

    var constructor = constructors
      .OrderByDescending(c => c.GetParameters().Length)
      .First();

    try
    {
      var parameters = ResolveParameterList(constructor);
      return (T)constructor.Invoke(parameters);
    }
    catch (Exception exception)
    {
      throw new DependencyResolutionException(
        $"Failed to create instance of type {typeof(T).FullName}. See inner exception for more information", exception);
    }
  }

  public T ExecuteMethod<T>(Delegate factoryMethod)
  {
    if (factoryMethod.Method.ReturnType != typeof(T))
      throw new Exception($"delegate has wrong return type"); // TODO exception type

    try
    {
      var parameters = ResolveParameterList(factoryMethod.Method);
      return (T)factoryMethod.DynamicInvoke(parameters)!;
    }
    catch (Exception exception)
    {
      throw new DependencyResolutionException(
        $"Failed to create instance of type {typeof(T).FullName}. See inner exception for more information", exception);
    }
  }

  public T Resolve<T>()
  {
    var instance = resolver.Resolve(typeof(T));
    if (instance.GetType().IsAssignableTo(typeof(T)))
      return (T)instance;
    throw new DependencyResolutionException($"Cannot resolve type {typeof(T).FullName}");
  }

  private object[] ResolveParameterList(MethodBase method) =>
    method.GetParameters().Select(parameter => resolver.Resolve(parameter.ParameterType)).ToArray();
}