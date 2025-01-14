using JetBrains.Annotations;

namespace Ember.DependencyInjection;

/// <summary>
/// Provides functionality to create instances of types.
/// </summary>
[PublicAPI]
public interface IActivator
{
  /// <summary>
  /// Creates an instance of the specified type.
  /// </summary>
  /// <typeparam name="T">The type of the instance to create.</typeparam>
  /// <returns>A new instance of the specified type.</returns>
  T CreateInstance<T>();

  T ExecuteMethod<T>(Delegate factoryMethod);
  T Resolve<T>();
}