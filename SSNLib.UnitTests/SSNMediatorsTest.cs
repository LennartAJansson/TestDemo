namespace SSNLib.UnitTests;

using Microsoft.Extensions.DependencyInjection;

using SSNApi.Domain;

using SSNLib;

[TestClass()]
public class SSNMediatorsTest
{
  public static IServiceProvider CreateServiceProvider()
  {
    return new ServiceCollection()
      .AddServices()
      .AddDomain()
      .BuildServiceProvider();
  }
}
