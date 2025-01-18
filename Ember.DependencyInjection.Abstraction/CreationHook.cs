namespace Ember.DependencyInjection;

/// <summary>
/// A method executed after an instance of an object is created.
/// </summary>
/// <param name="instance">The created object instance.</param>
public delegate void CreationHook(object instance);