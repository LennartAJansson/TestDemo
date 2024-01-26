namespace SSNLib.UnitTests;

using SSNLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;


[TestClass()]
public class SSNServicesTests
{
  [TestMethod()]
  public async Task IsValidSSNTestAsync()
  {
    //ARRANGE
    SSNServices service = new();
    var ssn = "800101-0019";
    
    //ACT
    bool actual = await service.IsValid(ssn);

    //ASSERT
    Assert.IsTrue(actual);
  }

  [TestMethod()]
  public async Task GetGenderIsFemaleTestAsync()
  {
    //ARRANGE
    SSNServices service = new();
    var ssn = "800101-1007";
    var expected = Gender.Female;

    //ACT
    var actual = await service.GetGender(ssn);

    //ASSERT
    Assert.AreEqual(actual, expected);
  }

  [TestMethod()]
  public async Task GetGenderIsMaleTestAsync()
  {
    //ARRANGE
    SSNServices service = new();
    var ssn = "800101-0017";
    var expected = Gender.Male;

    //ACT
    var actual = await service.GetGender(ssn);

    //ASSERT
    Assert.AreEqual(actual, expected);
  }

  [TestMethod()]
  public async Task GenerateRandomIsValidTestAsync()
  {
    //ARRANGE
    SSNServices service = new();
    DateTime start = new DateTime(1960, 1, 1);

    //ACT
    var ssn = await service.GenerateRandom(start);
    bool valid = await service.IsValid(ssn);

    //ASSERT
    Assert.IsTrue(valid);
  }

  [TestMethod()]
  public async Task GenerateCheckDigitTestAsync()
  {
    //ARRANGE
    SSNServices service = new();
    var ssn = "800101-001";
    var expected = 9;

    //ACT
    var actual = await service.GenerateCheckDigit(ssn);

    //ASSERT
    Assert.AreEqual(actual, expected);
  }
}