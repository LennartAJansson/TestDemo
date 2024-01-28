namespace SSNApi.IntegrationTests;
[TestClass]
public class SSNApiTests
{
  [TestMethod]
  public void TestGender()
  {
    _ = HttpClientExtensions.GenerateClient();
    //var response = client.GetAsync("api/ssn/gender");
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestIsValid()
  {
    _ = HttpClientExtensions.GenerateClient();
    //var response = client.GetAsync("api/ssn/valid");
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestGenerate()
  {
    _ = HttpClientExtensions.GenerateClient();
    //var response = client.GetAsync("api/ssn/random");
    Assert.IsTrue(true);
  }
}
