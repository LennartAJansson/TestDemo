namespace SSNApi.Domain.Mediators;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;

using static SSNApi.Domain.Mediators.DateOfBirthMediator;

public class DateOfBirthMediator(ILogger<DateOfBirthMediator> logger, ISSNServices services)
  : IRequestHandler<DateOfBirthRequest, DateOfBirthResponse>
{
  public record DateOfBirthRequest(string SSN) : IRequest<DateOfBirthResponse>
  {
    public static DateOfBirthRequest Create(string ssn) => new(ssn);
  }

  public record DateOfBirthResponse(DateOnly Date)
  {
    public static DateOfBirthResponse Create(DateOnly date) => new(date);
  };
  public async Task<DateOfBirthResponse> Handle(DateOfBirthRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Checking SSN: {ssn}", request.SSN);
    return DateOfBirthResponse.Create(await services.GetDateOfBirth(request.SSN));
  }
}
