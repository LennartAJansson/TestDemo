namespace SSNApi.Attributes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter, IEndpointFilter
{
  private const string APIKEYNAME = "ApiKey";
  private const string HEADERNAME = "x-api-key";

  //This is for Minimal Api's
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    IResult result = await CheckAndVerifyApiKey(context.HttpContext);

    return result is Ok ? await next.Invoke(context) : result;

  }

  //This is for Controllers
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

  private static Task<IResult> CheckAndVerifyApiKey(HttpContext context)
  {
    IConfiguration appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
    string? apiKey = appSettings.GetValue<string>(APIKEYNAME);

    if (!context.Request.Headers.TryGetValue(HEADERNAME, out Microsoft.Extensions.Primitives.StringValues extractedApiKey))
    {
      return Task.FromResult(Results.Json(new { StatusCode = 401, Content = "Api Key was not provided. (Using ApiKeyAttribute)" }, statusCode: 401));
    }

    return apiKey is null || !apiKey.Equals(extractedApiKey)
        ? Task.FromResult(Results.Json(new { StatusCode = 401, Content = "Api Key was invalid. (Using ApiKeyAttribute)" }, statusCode: 401))
        : Task.FromResult(Results.Ok());
  }
}

