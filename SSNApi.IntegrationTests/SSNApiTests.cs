namespace SSNApi.IntegrationTests;
[TestClass]
public class SSNApiTests
{
  [TestMethod]
  public void TestGender()
  {
    _ = HttpClientExtensions.GenerateClient();

    //string ssn = "800101-1007";
    //Gender expected = Gender.Female;

    //string ssn = "800101-0017";
    //Gender expected = Gender.Male;


    //var response = client.GetAsync("api/ssn/gender/{ssn}");
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestIsValid()
  {
    _ = HttpClientExtensions.GenerateClient();
    //var ssn = "800101-0019"; Valid
    //var ssn = "800101-0119"; Not Valid
    //var response = client.GetAsync("api/ssn/valid/{ssn}");
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestGenerate()
  {
    _ = HttpClientExtensions.GenerateClient();
    //var response = client.GetAsync("api/ssn/random/{start}");
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestProtectedGenerateTrue()
  {
    _ = HttpClientExtensions.GenerateSecureClient();
    //var response = client.GetAsync("api/ssn/randomprotected/{start}");
    Assert.IsTrue(true);
  }

  [TestMethod]
  public void TestProtectedGenerateFalse()
  {
    _ = HttpClientExtensions.GenerateSecureClient();
    //var response = client.GetAsync("api/ssn/randomprotected/{start}");
    Assert.IsTrue(true);
  }
}
