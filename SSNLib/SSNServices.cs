namespace SSNLib;

using SSNApi.Domain.Interfaces;
using SSNApi.Domain.Types;

//TODO: return Resonse from each method

public record Response(int ErrorCode, string ErrorMessage, object Value);

public class SSNServices : ISSNServices
{
    private readonly Random gen = new();

  public Task<int> GenerateCheckDigit(string ssn)
  {
    string s = ssn.Replace("-", "").Replace("+", "");
    if (s.Length != 9 || !long.TryParse(s, out _))
    {
      return Task.FromResult(0);
    }

    string number = s[..9];
    int sum = 0;

    for (int i = 0; i < 9; i++)
    {
      int v = int.Parse(number.Substring(i, 1));
      if (i % 2 == 0)
      {
        int t = v * 2;
        sum += (t / 10) + (t % 10);
      }
      else
      {
        sum += v;
      }
    }

    int above = (sum + 9) / 10 * 10;

    return Task.FromResult(above - sum);
  }

  public async Task<string> GenerateRandom(DateTime start)
  {
    int range = (DateTime.Today - start).Days;
    DateTime randomDate = start.AddDays(gen.Next(range));
    string ssn = $"{randomDate:yyMMdd}-{gen.Next(0, 999)}";
    //string ssn = $"{randomDate:yyMMdd}-{gen.Next(0, 999).ToString("D3")}";
    int check = await GenerateCheckDigit(ssn);
    await Console.Out.WriteLineAsync($"'{ssn}'");
    await Console.Out.WriteLineAsync($"'{ssn}{check}'");

    return $"{ssn}{check}";
  }

  public async Task<int> GetAge(string ssn)
  {
    DateTime birth = await GetDateOfBirth(ssn);

    return DateTime.Now.Day - birth.Day;
  }

  public Task<DateTime> GetDateOfBirth(string ssn)
  {
    string s = ssn.Replace("-", "").Replace("+", "");

    if (s.Length != 10 || !long.TryParse(s, out _))
    {
      return Task.FromResult(DateTime.MinValue);
    }

    int year = int.Parse(s[..2]);
    int month = int.Parse(s.Substring(2, 2));
    int day = int.Parse(s.Substring(4, 2));

    if (ssn.StartsWith('+'))
    {
      year += 1800;
    }
    else if (ssn.StartsWith('-'))
    {
      year += 1900;
    }
    else
    {
      year += 2000;
    }

    return Task.FromResult(new DateTime(year, month, day));
  }

  public Task<Gender> GetGender(string ssn)
  {
    string s = ssn.Replace("-", "").Replace("+", "");

    if (s.Length != 10 || !long.TryParse(s, out _))
    {
      return Task.FromResult(Gender.Unknown);
    }
    else
    {
      return !int.TryParse(s.AsSpan(8, 1), out int gender)
        ? Task.FromResult(Gender.Unknown)
        : gender % 2 == 0 ? Task.FromResult(Gender.Female) : Task.FromResult(Gender.Male);
    }
  }

  public Task<bool> IsValid(string ssn)
  {
    string s = ssn.Replace("-", "").Replace("+", "");

    if (s.Length != 10 || !long.TryParse(s, out _))
    {
      return Task.FromResult(false);
    }

    string check = s.Substring(9, 1);
    string number = s[..9];
    int sum = 0;

    for (int i = 0; i < 9; i++)
    {
      int v = int.Parse(number.Substring(i, 1));
      if (i % 2 == 0)
      {
        int t = v * 2;

        sum += (t / 10) + (t % 10);
      }
      else
      {
        sum += v;
      }
    }

    return Task.FromResult((sum + int.Parse(check)) % 10 == 0);
  }
}
