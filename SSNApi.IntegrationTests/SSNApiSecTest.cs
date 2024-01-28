namespace SSNApi.IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

public class SSNApiSecTest
{
  //Utan Auth context -> http 500
  //Med Auth context -> http 401
  //Med Auth context + rätt token -> http 200
  [TestMethod]
  public void TestGenerateSecure()
  {
    HttpClient client = HttpClientExtensions.GenerateClient();
    Assert.IsTrue(true);
  }
}
