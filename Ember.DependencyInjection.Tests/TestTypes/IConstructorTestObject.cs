﻿namespace Ember.DependencyInjection.Tests.TestTypes;

public interface IConstructorTestObject
{
  bool UsedCorrectConstructor { get; }
}

public class ConstructorTestObjectA : IConstructorTestObject
{
  public bool UsedCorrectConstructor => true;
}

public class ConstructorTestObjectB : IConstructorTestObject
{
  public bool UsedCorrectConstructor { get; }

  public ConstructorTestObjectB()
  {
    UsedCorrectConstructor = true;
  }
}

public class ConstructorTestObjectC : IConstructorTestObject
{
  public bool UsedCorrectConstructor { get; }

  public ConstructorTestObjectC() { }

  public ConstructorTestObjectC(ServiceA serviceA)
  {
    UsedCorrectConstructor = true;
  }
}

public class ConstructorTestObjectD : IConstructorTestObject
{
  public bool UsedCorrectConstructor { get; }

  public ConstructorTestObjectD()
  {
    UsedCorrectConstructor = true;
  }

  private ConstructorTestObjectD(ServiceA serviceA) { }
  protected ConstructorTestObjectD(ServiceB serviceB) { }
  internal ConstructorTestObjectD(ServiceA serviceA, ServiceB serviceB) { }
}