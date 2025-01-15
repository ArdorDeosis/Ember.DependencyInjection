namespace Ember.DependencyInjection;

/// <summary>
/// Marks the constructor as the one to be used for constructor injection.
/// </summary>
[AttributeUsage(AttributeTargets.Constructor)]
public class InjectedConstructorAttribute : Attribute;