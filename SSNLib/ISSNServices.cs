namespace SSNLib;

//Create a class called SSNServices
//It should be able to calculate and validate swedish social security numbers
public interface ISSNServices
{
  Task<int> GenerateCheckDigit(string ssn);
  Task<bool> IsValid(string ssn);
  Task<Gender> GetGender(string ssn);
  Task<int> GetAge(string ssn);
  Task<DateTime> GetDateOfBirth(string ssn);
  Task<string> GenerateRandom(DateTime start);
}
