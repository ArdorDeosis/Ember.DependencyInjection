using Fyremoss.DependencyInjection.Tests;

namespace Fyremoss.DependencyInjection.PropertyInjection.Tests;

public class FailingTestClass
{
  [Inject] public IService Property { get; } = null!;
}