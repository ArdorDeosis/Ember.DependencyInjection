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

  /// <summary>
  /// Executes the specified factory method to create an instance of type <typeparamref name="T"/>.
  /// </summary>
  /// <typeparam name="T">The type of the instance to create.</typeparam>
  /// <param name="factoryMethod">The delegate representing the factory method to execute.</param>
  /// <returns>An instance of type <typeparamref name="T"/> created by the factory method.</returns>
  T ExecuteMethod<T>(Delegate factoryMethod);

  /// <summary>
  /// Resolves an instance of the specified type.
  /// </summary>
  /// <typeparam name="T">The type of the instance to resolve.</typeparam>
  /// <returns>An instance of the specified type.</returns>
  T Resolve<T>();
}