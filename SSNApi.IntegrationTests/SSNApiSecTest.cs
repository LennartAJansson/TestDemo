namespace SSNApi.IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SSNApiSecTest
{
  //Utan Auth context -> http 500
  //Med Auth context -> http 401
  //Med Auth context + rätt token -> http 200
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
}
