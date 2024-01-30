namespace SSNApi.Domain.Mediators;

using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;

using static SSNApi.Domain.Mediators.GenerateRandomSSNMediator;

public class GenerateRandomSSNMediator(ILogger<GenerateRandomSSNMediator> logger, ISSNServices services) 
  : IRequestHandler<GenerateRandomSSNRequest, GenerateRandomSSNResponse>
{
  public record GenerateRandomSSNRequest(DateTime Start) : IRequest<GenerateRandomSSNResponse>
  {
    public static GenerateRandomSSNRequest Create(DateTime start) => new(start);
  }

  public record GenerateRandomSSNResponse(string SSN)
  {
    public static GenerateRandomSSNResponse Create(string ssn) => new(ssn);
  };

  public async Task<GenerateRandomSSNResponse> Handle(GenerateRandomSSNRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Generating SSN: {start}", request.Start);
    return GenerateRandomSSNResponse.Create(await services.GenerateRandom(request.Start));
  }
}
