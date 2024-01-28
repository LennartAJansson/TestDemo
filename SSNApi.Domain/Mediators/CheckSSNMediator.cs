namespace SSNApi.Domain.Mediators;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;

using static SSNApi.Domain.Mediators.CheckSSNMediator;

public class CheckSSNMediator(ILogger<CheckSSNMediator> logger, ISSNServices services) 
  : IRequestHandler<CheckSSNRequest, CheckSSNResponse>
{
  public record CheckSSNRequest(string SSN) : IRequest<CheckSSNResponse>
  {
    public static CheckSSNRequest Create(string ssn) => new(ssn);
  }

  public record CheckSSNResponse(bool IsValid)
  {
    public static CheckSSNResponse Create(bool isValid) => new(isValid);
  };

  public async Task<CheckSSNResponse> Handle(CheckSSNRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Checking SSN: {ssn}", request.SSN);
    return CheckSSNResponse.Create(await services.IsValid(request.SSN));
  }
}
