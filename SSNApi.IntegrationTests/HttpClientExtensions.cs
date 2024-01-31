namespace SSNApi.IntegrationTests;

using System.Net.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class HttpClientExtensions
{
  public static IServiceProvider CreateServiceProvider()
  {
    IHost host = Host.CreateDefaultBuilder()
      .ConfigureAppConfiguration((hostingContext, config) =>
      {
        _ = config.AddUserSecrets<SSNApiTests>();
      })
      .ConfigureLogging(x =>
      {
        x.ClearProviders();
        x.AddConsole();
      })
      .ConfigureServices((hostContext, services) =>
      {
        _ = services.AddHttpClient();
        _ = services.AddHttpClient("TestClient", ConfigureHttpOptions);
        _ = services.AddHttpClient("SecureTestClient", ConfigureSecureHttpOptions);
      })
      .Build();

    return host.Services;
  }

  private static void ConfigureHttpOptions(IServiceProvider provider, HttpClient client)
  {
    string baseUrl = provider.GetRequiredService<IConfiguration>().GetValue<string>("ASPNETCORE_URLS")
      ?? throw new ArgumentException("No BaseUrl");

    var logger = provider.GetRequiredService<ILogger<SSNApiTests>>();
    logger.LogInformation($"Using BaseUrl: {baseUrl}");

    client.BaseAddress = new Uri(baseUrl);
  }

  private static void ConfigureSecureHttpOptions(IServiceProvider provider, HttpClient client)
  {
    string baseUrl = provider.GetRequiredService<IConfiguration>().GetValue<string>("ASPNETCORE_URLS")
      ?? throw new ArgumentException("No BaseUrl");

    var logger = provider.GetRequiredService<ILogger<SSNApiTests>>();
    logger.LogInformation($"Using BaseUrl: {baseUrl}");

    string apiKey = provider.GetRequiredService<IConfiguration>().GetValue<string>("API_KEY")
      ?? throw new ArgumentException("No ApiKey");

    logger.LogInformation($"Using ApiKey: {apiKey}");

    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Add("x-api-key", apiKey);
  }

  public static HttpClient GenerateClient()
  {
    IServiceProvider serviceProvider = CreateServiceProvider();

    HttpClient httpClient = serviceProvider
      .GetRequiredService<IHttpClientFactory>()
      .CreateClient("TestClient");

    return httpClient;
  }

  public static HttpClient GenerateSecureClient()
  {
    IServiceProvider serviceProvider = CreateServiceProvider();

    HttpClient httpClient = serviceProvider
      .GetRequiredService<IHttpClientFactory>()
      .CreateClient("SecureTestClient");

    return httpClient;
  }
}
