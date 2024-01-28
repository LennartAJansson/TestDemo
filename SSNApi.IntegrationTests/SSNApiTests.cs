namespace SSNApi.IntegrationTests;

[TestClass]
public class SSNApiTests
{
  public static HttpClient GenerateClient()
  {
    var baseUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URLS", EnvironmentVariableTarget.Process)
      ?? throw new ArgumentException("Env variable ASPNETCORE_URLS is missing");
    var apiKey = Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.Process)
      ?? throw new ArgumentException("Env variable API_KEY is missing");

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
  }

  [TestMethod]
  public void TestIsValid()
  {
    var client = GenerateClient();
  }
}