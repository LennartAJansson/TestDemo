namespace SSNApi.Attributes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;

//Attribute that serves two purposes:
//1. For Minimal Api's it is an EndpointFilter
//2. For Controllers it is an ActionFilter
//It checks if the ApiKey is valid and if not it returns a 401 Unauthorized
//It is added in the SSNApi project in the Program.cs file through the UseApiKey extension method
//  found in MinimalApiExtensions.cs in the Extensions folder
//It is used in the SSNApi project in the Controllers/SSNController.cs file

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter, IEndpointFilter
{
  private const string APIKEYNAME = "Api_Key";
  private const string HEADERNAME = "x-api-key";

  //This is for Minimal Api's:
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    IResult result = await CheckAndVerifyApiKey(context.HttpContext);

    return result is Ok ? await next.Invoke(context) : result;

  }

  //This is for Controllers:
  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    IResult result = await CheckAndVerifyApiKey(context.HttpContext);

    if (result is Ok)
    {
      _ = await next();
    }
    else
    {
      await result.ExecuteAsync(context.HttpContext);
    }
  }

  //Internal method that checks if the ApiKey is valid, reads the value Api_Key from configuration
  private static Task<IResult> CheckAndVerifyApiKey(HttpContext context)
  {
    IConfiguration appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
    string? apiKey = appSettings.GetValue<string>(APIKEYNAME);
    if (apiKey == null)
    {
      apiKey = Environment.GetEnvironmentVariable(APIKEYNAME);
    }

    if (!context.Request.Headers.TryGetValue(HEADERNAME, out Microsoft.Extensions.Primitives.StringValues extractedApiKey))
    {
      return Task.FromResult(Results.Json(new { StatusCode = 401, Content = "Api Key was not provided. (Using ApiKeyAttribute)" }, statusCode: 401));
    }

    return apiKey is null || !apiKey.Equals(extractedApiKey)
        ? Task.FromResult(Results.Json(new { StatusCode = 401, Content = "Api Key was invalid. (Using ApiKeyAttribute)" }, statusCode: 401))
        : Task.FromResult(Results.Ok());
  }
}

