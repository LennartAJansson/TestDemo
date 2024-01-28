namespace SSNApi.Domain.Interfaces;

using SSNApi.Domain.Types;

public interface ISSNServices
{
  Task<int> GenerateCheckDigit(string ssn);
  Task<bool> IsValid(string ssn);
  Task<Gender> GetGender(string ssn);
  Task<int> GetAge(string ssn);
  Task<DateTime> GetDateOfBirth(string ssn);
  Task<string> GenerateRandom(DateTime start);
}
