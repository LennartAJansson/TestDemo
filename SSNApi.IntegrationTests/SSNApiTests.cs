namespace SSNApi.IntegrationTests;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using static SSNApi.Domain.Mediators.GenerateRandomSSNMediator;
using static SSNApi.Domain.Mediators.IsValidMediator;

[TestClass]
public class SSNApiTests
{
  [TestMethod]
  public async Task TestIsValidAsync()
  {
    //ARRANGE
    HttpClient client = HttpClientExtensions.GenerateClient();
    string ssn = "800101-0019"; //Valid

    //ACT
    HttpResponseMessage response = await client.GetAsync($"api/ssn/isvalid/{ssn}");
    string json = await response.Content.ReadAsStringAsync();
    IsValidResponse? result = JsonSerializer.Deserialize<IsValidResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    //ASSERT
    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    Assert.IsNotNull(result);
    Assert.IsTrue(result.IsValid);
  }

  [TestMethod]
  public async Task TestIsNotValidAsync()
  {
    //ARRANGE
    HttpClient client = HttpClientExtensions.GenerateClient();
    string ssn = "800101-0119"; //Not Valid

    //ACT
    HttpResponseMessage response = await client.GetAsync($"api/ssn/isvalid/{ssn}");
    string json = await response.Content.ReadAsStringAsync();
    IsValidResponse? result = JsonSerializer.Deserialize<IsValidResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    //ASSERT
    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    Assert.IsNotNull(result);
    Assert.IsFalse(result.IsValid);
  }

  ////Utan Auth context -> http 500
  ////Med Auth context -> http 401
  ////Med Auth context + rätt token -> http 200
  [TestMethod]
  public async Task TestGenerateSecureIncorrectClientAsync()
  {
    //ARRANGE
    HttpClient client = HttpClientExtensions.GenerateClient();
    string date = "1960-01-01";

    //ACT
    HttpResponseMessage response = await client.GetAsync($"api/ssn/generaterandomprotected/{date}");
    string json = await response.Content.ReadAsStringAsync();
    GenerateRandomSSNResponse? result = JsonSerializer.Deserialize<GenerateRandomSSNResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    //ASSERT
    Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);
  }

  [TestMethod]
  public async Task TestGenerateSecureCorrectClientAsync()
  {
    //ARRANGE
    HttpClient client = HttpClientExtensions.GenerateSecureClient();
    string date = "1960-01-01";

    //ACT
    HttpResponseMessage response = await client.GetAsync($"api/ssn/generaterandomprotected/{date}");
    string json = await response.Content.ReadAsStringAsync();
    GenerateRandomSSNResponse? result = JsonSerializer.Deserialize<GenerateRandomSSNResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    //ASSERT
    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    Assert.IsNotNull(result);
    Assert.IsFalse(string.IsNullOrEmpty(result.SSN));
  }

}
