// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SSNApi.Controllers;

using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using SSNApi.Attributes;

using static SSNApi.Domain.Mediators.CheckSSNIsValidMediator;
using static SSNApi.Domain.Mediators.GenerateRandomSSNMediator;
using static SSNApi.Domain.Mediators.GetGenderMediator;

[Route("api/[controller]")]
[ApiController]
public class SSNController(ILogger<SSNController> logger, ISender sender) : ControllerBase
{
  [HttpGet("gender/{ssn}")]
  public async Task<GetGenderResponse> GenderAsync(string ssn)
  {
    logger.LogInformation("GenderAsync called with {ssn}", ssn);
    return await sender.Send(GetGenderRequest.Create(ssn));
  }

  [HttpGet("valid/{ssn}")]
  public async Task<CheckSSNIsValidResponse> IsValidAsync(string ssn)
  {
    logger.LogInformation("IsValidAsync called with {ssn}", ssn);
    return await sender.Send(CheckSSNIsValidRequest.Create(ssn));
  }

  [HttpGet("random/{start}")]
  public async Task<GenerateSSNResponse> Random(DateTime start)
  {
    logger.LogInformation("Random called with {start}", start);
    return await sender.Send(GenerateSSNRequest.Create(start));
  }

  [ApiKey()]
  [HttpGet("randomprotected/{start}")]
  public async Task<GenerateSSNResponse> RandomProtected(DateTime start)
  {
    logger.LogInformation("RandomProtected called with {start}", start);
    return await sender.Send(GenerateSSNRequest.Create(start));
  }
}
