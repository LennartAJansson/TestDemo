namespace SSNApi.Domain.Mediators;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;
using SSNApi.Domain.Types;

using static SSNApi.Domain.Mediators.GenderMediator;

public class GenderMediator(ILogger<GenderMediator> logger, ISSNServices services) 
  : IRequestHandler<GenderRequest, GenderResponse>
{
  public record GenderRequest(string SSN) : IRequest<GenderResponse>
  {
    public static GenderRequest Create(string ssn) => new(ssn);
  }

  public record GenderResponse(Gender Gender)
  {
    public static GenderResponse Create(Gender gender) => new(gender);
  };

  public async Task<GenderResponse> Handle(GenderRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Checking Gender: {ssn}", request.SSN);
    return GenderResponse.Create(await services.GetGender(request.SSN));
  }
}
