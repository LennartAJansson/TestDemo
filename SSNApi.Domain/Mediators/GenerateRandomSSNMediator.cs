namespace SSNApi.Domain.Mediators;

using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;

using static SSNApi.Domain.Mediators.GenerateRandomSSNMediator;

public class GenerateRandomSSNMediator(ILogger<GenerateRandomSSNMediator> logger, ISSNServices services) 
  : IRequestHandler<GenerateSSNRequest, GenerateSSNResponse>
{
  public record GenerateSSNRequest(DateTime Start) : IRequest<GenerateSSNResponse>
  {
    public static GenerateSSNRequest Create(DateTime start) => new(start);
  }

  public record GenerateSSNResponse(string SSN)
  {
    public static GenerateSSNResponse Create(string ssn) => new(ssn);
  };

  public async Task<GenerateSSNResponse> Handle(GenerateSSNRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Generating SSN: {start}", request.Start);
    return GenerateSSNResponse.Create(await services.GenerateRandom(request.Start));
  }
}
