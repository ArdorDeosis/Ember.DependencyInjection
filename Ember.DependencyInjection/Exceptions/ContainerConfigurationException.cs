namespace Ember.DependencyInjection.Exceptions;

public class ContainerConfigurationException : Exception
{
  public ContainerConfigurationException(string message) : base(message) { }
  public ContainerConfigurationException(string message, Exception innerException) : base(message, innerException) { }
}