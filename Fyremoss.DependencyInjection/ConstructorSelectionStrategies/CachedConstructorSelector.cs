using System.Reflection;

namespace Fyremoss.DependencyInjection;

internal class CachedConstructorSelector
{
  private readonly Dictionary<Type, ConstructorInfo?> cache = new();
  private readonly ConstructorSelector constructorSelector;

  public CachedConstructorSelector(ConstructorSelector constructorSelector)
  {
    this.constructorSelector = constructorSelector;
  }

  public ConstructorInfo? GetConstructor(Type type)
  {
    if (!cache.ContainsKey(type))
      cache[type] = constructorSelector(type);
    return cache[type];
  }
}