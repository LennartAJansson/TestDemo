namespace SSNApi.IntegrationTests;

using Microsoft.Extensions.DependencyInjection;

[TestClass]
public class SSNApiTests
{
  public static IServiceProvider CreateServiceProvider()
  {
    return new ServiceCollection()
      .BuildServiceProvider();
  }

  public static HttpClient GenerateClient()
  {
    var baseUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URLS", EnvironmentVariableTarget.Process)
      ?? throw new ArgumentException("Env variable ASPNETCORE_URLS is missing");
    var apiKey = Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.Process)
      ?? throw new ArgumentException("Env variable API_KEY is missing");

    Console.WriteLine(baseUrl);
    Console.WriteLine(apiKey);

    var httpClient = new HttpClient()
    {
      DefaultRequestHeaders =
      {
        { "X-API-KEY", apiKey }
      },
      BaseAddress = new Uri(baseUrl)
    }; 

    return httpClient;
  }

  [TestMethod]
  public void TestGender()
  {
    var client = GenerateClient();
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestIsValid()
  {
    var client = GenerateClient();
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestGenerate()
  {
    var client = GenerateClient();
    Assert.IsTrue(true);
  }
}