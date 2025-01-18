using Fyremoss.DependencyInjection.Tests;

namespace Fyremoss.DependencyInjection.PropertyInjection.Tests;

public class TestClass
{
  [Inject] public IService PublicSet { get; set; } = null!;
  [Inject] public IService PublicInit { get; init; } = null!;
  [Inject] public IService PrivateSet { get; private set; } = null!;
  [Inject] public IService PrivateInit { get; private init; } = null!;

  internal bool SetterOnlyHasBeenSet = false;

  [Inject]
  internal IService SetterOnly
  {
    set => SetterOnlyHasBeenSet = true;
  }
}