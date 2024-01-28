namespace SSNApi.Domain.Mediators;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;

using static SSNApi.Domain.Mediators.CheckSSNIsValidMediator;

public class CheckSSNIsValidMediator(ILogger<CheckSSNIsValidMediator> logger, ISSNServices services) 
  : IRequestHandler<CheckSSNIsValidRequest, CheckSSNIsValidResponse>
{
  public record CheckSSNIsValidRequest(string SSN) : IRequest<CheckSSNIsValidResponse>
  {
    public static CheckSSNIsValidRequest Create(string ssn) => new(ssn);
  }

  public record CheckSSNIsValidResponse(bool IsValid)
  {
    public static CheckSSNIsValidResponse Create(bool isValid) => new(isValid);
  };

  public async Task<CheckSSNIsValidResponse> Handle(CheckSSNIsValidRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Checking SSN: {ssn}", request.SSN);
    return CheckSSNIsValidResponse.Create(await services.IsValid(request.SSN));
  }
}
