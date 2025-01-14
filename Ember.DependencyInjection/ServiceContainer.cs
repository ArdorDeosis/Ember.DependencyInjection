﻿using System.Reflection;

namespace Ember.DependencyInjection;

/// <inheritdoc />
internal class ServiceContainer : IActivator
{
  private readonly DependencyResolver resolver;

  internal ServiceContainer(ContractRegistry registry)
  {
    registry.Add<IActivator>().ToInstance(this);
    resolver = new DependencyResolver(registry.MakeContractSet(this));
  }

  /// <inheritdoc />
  public T CreateInstance<T>()
  {
    // TODO: enhance constructor selection
    if (typeof(T).GetConstructors() is not { Length: > 0 } constructors)
      throw new ArgumentException($"Cannot create an instance of type {typeof(T).FullName}; the type has no public constructor.");

    var constructor = constructors
      .OrderByDescending(constructorInfo => constructorInfo.GetParameters().Length)
      .First();

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