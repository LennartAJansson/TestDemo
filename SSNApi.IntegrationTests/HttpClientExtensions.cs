namespace SSNApi.IntegrationTests;

using System.Net.Http;

using Microsoft.Extensions.DependencyInjection;

public static class HttpClientExtensions
{
  public static IServiceProvider CreateServiceProvider()
  {
    ServiceCollection services = new();
    _ = services.AddHttpClient("TestClient", ConfigureHttpOptions);

    return services.BuildServiceProvider();
  }

  private static void ConfigureHttpOptions(IServiceProvider provider, HttpClient client)
  {
    string baseUrl = Environment.GetEnvironmentVariable("ASPNETCORE_URLS", EnvironmentVariableTarget.Process)
      ?? throw new ArgumentException("Env variable ASPNETCORE_URLS is missing");
    string apiKey = Environment.GetEnvironmentVariable("API_KEY", EnvironmentVariableTarget.Process)
      ?? throw new ArgumentException("Env variable API_KEY is missing");

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("x-api-key", apiKey);
  }

  public static HttpClient GenerateClient()
  {
    IServiceProvider serviceProvider = CreateServiceProvider();

    HttpClient httpClient = serviceProvider.GetRequiredService<HttpClient>();

    return httpClient;
  }

}
