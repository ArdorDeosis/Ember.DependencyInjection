namespace Fyremoss.DependencyInjection;

/// <summary>
/// A method executed after an instance of an object is created.
/// </summary>
/// <param name="injector">An instance of the injector this hook runs on.</param>
/// <param name="instance">The created object instance.</param>
public delegate void CreationHook(IInjector injector, object instance);