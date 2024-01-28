namespace SSNApi.Domain.Mediators;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using SSNApi.Domain.Interfaces;

using static SSNApi.Domain.Mediators.GetGenderMediator;

public class GetGenderMediator(ILogger<GetGenderMediator> logger, ISSNServices services) 
  : IRequestHandler<GetGenderRequest, GetGenderResponse>
{
  public record GetGenderRequest(string SSN) : IRequest<GetGenderResponse>
  {
    public static GetGenderRequest Create(string ssn) => new(ssn);
  }

  public record GetGenderResponse(string Gender)
  {
    public static GetGenderResponse Create(string gender) => new(gender);
  };

  public async Task<GetGenderResponse> Handle(GetGenderRequest request, CancellationToken cancellationToken)
  {
    logger.LogInformation("Checking Gender: {ssn}", request.SSN);
    return GetGenderResponse.Create((await services.GetGender(request.SSN)).ToString());
  }
}
