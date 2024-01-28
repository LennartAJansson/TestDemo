namespace SSNLib.UnitTests;
using SSNApi.Domain.Types;

using SSNLib;


[TestClass()]
public class SSNServicesTests
{
  [TestMethod()]
  public async Task IsValidSSNTestAsync()
  {
    //ARRANGE
    SSNServices service = new();
    string ssn = "800101-0019";

    //ACT
    bool actual = await service.IsValid(ssn);

    //ASSERT
    Assert.IsTrue(actual);
  }

  [TestMethod()]
  public async Task IsNotValidSSNTestAsync()
  {
    //ARRANGE
    SSNServices service = new();
    string ssn = "800101-0119";

    //ACT
    bool actual = await service.IsValid(ssn);

    //ASSERT
    Assert.IsFalse(actual);
  }

  [TestMethod()]
  public async Task GetGenderIsFemaleTestAsync()
  {
    //ARRANGE
    SSNServices service = new SSNServices();
    string ssn = "800101-1007";
    Gender expected = Gender.Female;

    //ACT
    Gender actual = await service.GetGender(ssn);

    //ASSERT
    Assert.AreEqual(actual, expected);
  }

  [TestMethod()]
  public async Task GetGenderIsMaleTestAsync()
  {
    //ARRANGE
    SSNServices service = new SSNServices();
    string ssn = "800101-0017";
    Gender expected = Gender.Male;

    //ACT
    Gender actual = await service.GetGender(ssn);

    //ASSERT
    Assert.AreEqual(actual, expected);
  }

  [TestMethod()]
  public async Task GenerateRandomIsValidTestAsync()
  {
    //ARRANGE
    SSNServices service = new SSNServices();
    DateTime start = new(1960, 1, 1);

    //ACT
    string ssn = await service.GenerateRandom(start);
    bool valid = await service.IsValid(ssn);

    //ASSERT
    Assert.IsTrue(valid);
  }

  [TestMethod()]
  public async Task GenerateCheckDigitTestAsync()
  {
    //ARRANGE
    SSNServices service = new SSNServices();
    string ssn = "800101-001";
    int expected = 9;

    //ACT
    int actual = await service.GenerateCheckDigit(ssn);

    //ASSERT
    Assert.AreEqual(actual, expected);
  }
}