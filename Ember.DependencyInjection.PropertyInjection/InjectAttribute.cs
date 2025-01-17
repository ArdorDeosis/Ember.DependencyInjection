using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// Marks a property to be injected with <see cref="InjectorExtensions.InjectProperties(IInjector, object)"/>.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Property)]
public class InjectAttribute : Attribute;