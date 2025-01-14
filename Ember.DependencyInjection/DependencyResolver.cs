using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using Ember.DependencyInjection.Exceptions;

namespace Ember.DependencyInjection;

internal class DependencyResolver
{
  private static readonly FrozenSet<Type> SupportedCollectionTypes = new HashSet<Type>
  {
    typeof(IEnumerable<>),
    typeof(IReadOnlyCollection<>),
    typeof(IReadOnlyList<>),
  }.ToFrozenSet();

  private readonly ContractSet contracts;

  public DependencyResolver(ContractSet contracts)
  {
    this.contracts = contracts;
  }

  public object Resolve(Type type)
  {
    if (TryResolveFirst(type, out var resolved))
      return resolved;

    if (TryResolveCollection(type, out var resolvedCollection))
      return resolvedCollection;

    throw new DependencyResolutionException($"Cannot resolve type {type.FullName}");
  }

  private bool TryResolveFirst(Type type, [NotNullWhen(true)] out object? instance)
  {
    if (contracts.TryGetFirst(type, out var contract))
    {
      instance = contract.Resolve();
      return true;
    }

    instance = null;
    return false;
  }

  private bool TryResolveCollection(Type collectionType, [NotNullWhen(true)] out Array? instances)
  {
    instances = null;
    return IsCollectionType(collectionType, out var itemType) && TryResolveAll(itemType, out instances);
  }

  private bool TryResolveAll(Type type, [NotNullWhen(true)] out Array? instances)
  {
    var contractList = contracts.Get(type);
    if (contractList.Any())
    {
      instances = contracts
        .Get(type)
        .Select(contract => contract.Resolve())
        .ToArray();
      return true;
    }
    instances = null;
    return false;
  }

  private static bool IsCollectionType(Type collectionType, [NotNullWhen(true)] out Type? itemType)
  {
    itemType = collectionType
      .GetInterfaces()
      .Append(collectionType) // include the type itself
      .FirstOrDefault(type => type.IsGenericType && SupportedCollectionTypes.Contains(type.GetGenericTypeDefinition()))?
      .GetGenericArguments()
      .FirstOrDefault();
    return itemType is not null;
  }
}