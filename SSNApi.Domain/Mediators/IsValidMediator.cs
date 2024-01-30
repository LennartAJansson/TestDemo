namespace SSNApi.Domain.Mediators;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;

using static SSNApi.Domain.Mediators.IsValidMediator;

public class IsValidMediator(ILogger<IsValidMediator> logger, ISSNServices services) 
  : IRequestHandler<IsValidRequest, IsValidResponse>
{
  public record IsValidRequest(string SSN) : IRequest<IsValidResponse>
  {
    public static IsValidRequest Create(string ssn) => new(ssn);
  }

  public record IsValidResponse(bool IsValid)
  {
    public static IsValidResponse Create(bool isValid) => new(isValid);
  };

  public async Task<IsValidResponse> Handle(IsValidRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Checking SSN: {ssn}", request.SSN);
    return IsValidResponse.Create(await services.IsValid(request.SSN));
  }
}
