namespace SSNApi.IntegrationTests;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using static SSNApi.Domain.Mediators.IsValidMediator;

[TestClass]
public class SSNApiTests
{
  [TestMethod]
  public async Task TestIsValidAsync()
  {
    var client = HttpClientExtensions.GenerateClient();
    var ssn = "800101-0019"; //Valid
    var response = await client.GetAsync($"api/ssn/isvalid/{ssn}");
    var json = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<IsValidResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    Assert.IsTrue(result.IsValid);
  }

  [TestMethod]
  public async Task TestIsNotValidAsync()
  {
    var client = HttpClientExtensions.GenerateClient();
    var ssn = "800101-0119"; //Not Valid
    var response = await client.GetAsync($"api/ssn/isvalid/{ssn}");
    var json = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<IsValidResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
    Assert.IsFalse(result.IsValid);
  }

  //[TestMethod]
  //public void TestProtectedGenerateTrue()
  //{
  //  _ = HttpClientExtensions.GenerateSecureClient();
  //  //var response = client.GetAsync("api/ssn/randomprotected/{start}");
  //  Assert.IsTrue(true);
  //}

  //[TestMethod]
  //public void TestProtectedGenerateFalse()
  //{
  //  _ = HttpClientExtensions.GenerateSecureClient();
  //  //var response = client.GetAsync("api/ssn/randomprotected/{start}");
  //  Assert.IsTrue(true);
  //}

  ////Utan Auth context -> http 500
  ////Med Auth context -> http 401
  ////Med Auth context + rätt token -> http 200
  //[TestMethod]
  //public void TestGenerateSecure()
  //{
  //  HttpClient client = HttpClientExtensions.GenerateClient();
  //  Assert.IsTrue(true);
  //}

}
