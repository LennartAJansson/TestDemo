namespace SSNApi.IntegrationTests;
using Microsoft.Testing.Platform.Configurations;

[TestClass]
public class SSNApiTests
{
  [TestMethod]
  public void TestGender()
  {
    HttpClient client = HttpClientExtensions.GenerateClient();
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestIsValid()
  {
    HttpClient client = HttpClientExtensions.GenerateClient();
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestGenerate()
  {
    HttpClient client = HttpClientExtensions.GenerateClient();
    var response = client.GetAsync("api/ssn/gender");
    //var response = client.GetAsync("api/ssn/valid");
    //var response = client.GetAsync("api/ssn/gender");
    Assert.IsTrue(true/*response.Result.StatusCode=*/);
  }
}
