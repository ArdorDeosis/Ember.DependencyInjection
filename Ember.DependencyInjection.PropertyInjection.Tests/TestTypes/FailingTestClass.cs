using Ember.DependencyInjection.Tests;

namespace Ember.DependencyInjection.PropertyInjection.Tests;

public class FailingTestClass
{
  [Inject] public IService Property { get; } = null!;
}