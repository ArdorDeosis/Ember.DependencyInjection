using System.Reflection;

namespace Fyremoss.DependencyInjection;

public delegate ConstructorInfo? ConstructorSelector(Type type);

public static class ConstructorSelectors
{
  public static ConstructorSelector MostParameters => type => type
    .GetConstructors()
    .OrderByDescending(constructorInfo => constructorInfo.GetParameters().Length)
    .FirstOrDefault();

  public static ConstructorSelector WithAttribute => type => type
      .GetConstructors()
      .Where(ctor => ctor.GetCustomAttribute<InjectedConstructorAttribute>() is not null)
      .ToArray() switch
    {
      [] => null,
      [var single] => single,
      _ => throw new DependencyResolutionException($"Type {type.FullName} has multiple constructors marked with attribute {nameof(InjectedConstructorAttribute)}."),
    };
}

public static class ConstructorSelectorsExtensions
{
  public static ConstructorSelector Then(this ConstructorSelector selector, ConstructorSelector fallback) =>
    type => selector(type) ?? fallback(type);
}