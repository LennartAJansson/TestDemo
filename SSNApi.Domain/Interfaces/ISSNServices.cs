namespace SSNApi.Domain.Interfaces;

using SSNApi.Domain.Types;

public interface ISSNServices
{
  Task<Gender> GetGender(string ssn);
  Task<int> GetAge(string ssn);
  Task<DateOnly> GetDateOfBirth(string ssn);
  Task<string> GenerateRandom(DateTime start);
  Task<bool> IsValid(string ssn);
  Task<int> CalculateCheckDigit(string ssn);
}
