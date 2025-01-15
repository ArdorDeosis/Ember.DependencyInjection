namespace Ember.DependencyInjection;

/// <summary>
/// Defines the source for an instance used in a <see cref="Contract{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the contract.</typeparam>
internal interface IInstanceSource<out T> where T : notnull
{
  /// <summary>
  /// Resolves an instance of type <typeparamref name="T"/>.
  /// </summary>
  /// <param name="activator">The activator used to create the instance.</param>
  T Resolve(IActivator activator);
}