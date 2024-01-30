namespace SSNLib.UnitTests;

using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SSNApi.Domain;
using SSNApi.Domain.Types;

using SSNLib;

using static SSNApi.Domain.Mediators.IsValidMediator;
using static SSNApi.Domain.Mediators.GenerateRandomSSNMediator;
using static SSNApi.Domain.Mediators.GenderMediator;

[TestClass()]
public class SSNMediatorsTest
{
  public static IServiceProvider CreateServiceProvider()
  {
    return new ServiceCollection()
      .AddLogging(builder=>builder.AddConsole())
      .AddServices()
      .AddDomain()
      .BuildServiceProvider();
  }

  [TestMethod]
  public async Task CheckSSNMediatorValidTestAsync()
  {
    var scope = CreateServiceProvider().CreateScope();

    var actual = await scope.ServiceProvider
      .GetRequiredService<ISender>()
      .Send(IsValidRequest.Create("800101-0019"));
    
    Assert.IsTrue(actual.IsValid);
  }

  [TestMethod]
  public async Task CheckSSNMediatorNotValidTestAsync()
  {
    var scope = CreateServiceProvider().CreateScope();

    var actual = await scope.ServiceProvider
      .GetRequiredService<ISender>()
      .Send(IsValidRequest.Create("800101-0119"));

    Assert.IsFalse(actual.IsValid);
  }

  [TestMethod]
  public async Task GenerateRandomSSNMediatorTestAsync()
  {
    var scope = CreateServiceProvider().CreateScope();

    var response = await scope.ServiceProvider
      .GetRequiredService<ISender>()
      .Send(GenerateRandomSSNRequest.Create(DateTime.Parse("1960-01-01")));

    var actual = await scope.ServiceProvider
      .GetRequiredService<ISender>()
      .Send(IsValidRequest.Create(response.SSN));

    Assert.IsTrue(actual.IsValid);
  }

  [TestMethod]
  public async Task GetGenderMediatorMaleTestAsync()
  {
    var scope = CreateServiceProvider().CreateScope();
    string ssn = "800101-0017";
    Gender expected = Gender.Male;

    var actual = await scope.ServiceProvider
      .GetRequiredService<ISender>()
      .Send(GenderRequest.Create(ssn));

    Assert.AreEqual(expected, actual.Gender);
  }
  
  [TestMethod]
  public async Task GetGenderMediatorFemaleTestAsync()
  {
    var scope = CreateServiceProvider().CreateScope();
    string ssn = "800101-1007";
    Gender expected = Gender.Female;

    var actual = await scope.ServiceProvider
      .GetRequiredService<ISender>()
      .Send(GenderRequest.Create(ssn));

    Assert.AreEqual(expected, actual.Gender);
  }
}
