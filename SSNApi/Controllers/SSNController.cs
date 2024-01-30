// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SSNApi.Controllers;

using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using SSNApi.Attributes;

using static SSNApi.Domain.Mediators.IsValidMediator;
using static SSNApi.Domain.Mediators.GenerateRandomSSNMediator;
using static SSNApi.Domain.Mediators.GenderMediator;
using static SSNApi.Domain.Mediators.DateOfBirthMediator;

[Route("api/[controller]")]
[ApiController]
public class SSNController(ILogger<SSNController> logger, ISender sender) : ControllerBase
{
  [HttpGet("gender/{ssn}")]
  public async Task<GenderResponse> GenderAsync(string ssn)
  {
    logger.LogInformation("GenderAsync called with {ssn}", ssn);
    return await sender.Send(GenderRequest.Create(ssn));
  }

  [HttpGet("isvalid/{ssn}")]
  public async Task<IsValidResponse> IsValidAsync(string ssn)
  {
    logger.LogInformation("IsValidAsync called with {ssn}", ssn);
    return await sender.Send(IsValidRequest.Create(ssn));
  }

  [HttpGet("dateofbirth/{ssn}")]
  public async Task<DateOfBirthResponse> DateOfBirth(string ssn)
  {
    logger.LogInformation("DateOfBirth called with {ssn}", ssn);
    return await sender.Send(DateOfBirthRequest.Create(ssn));
  }

  [HttpGet("generaterandom/{start}")]
  public async Task<GenerateRandomSSNResponse> GenerateRandom(DateTime start)
  {
    logger.LogInformation("GenerateRandom called with {start}", start);
    return await sender.Send(GenerateRandomSSNRequest.Create(start));
  }

  [ApiKey()]
  [HttpGet("generaterandomprotected/{start}")]
  public async Task<GenerateRandomSSNResponse> GenerateRandomProtected(DateTime start)
  {
    logger.LogInformation("GenerateRandomProtected called with {start}", start);
    return await sender.Send(GenerateRandomSSNRequest.Create(start));
  }
}
