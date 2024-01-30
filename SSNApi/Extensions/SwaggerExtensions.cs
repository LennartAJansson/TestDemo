using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerExtensions
{
  //Modifies the SwaggerGenOptions to add the ApiKey security definition and requirement
  public static SwaggerGenOptions AddSwaggerGenOptions(this SwaggerGenOptions options)
  {
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceName", Version = "1" });
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
      Name = "x-api-key",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.ApiKey,
      Description = "Authorization by x-api-key inside request's header",
      Scheme = "ApiKeyScheme"
    });

    OpenApiSecurityScheme key = new()
    {
      Reference = new OpenApiReference
      {
        Type = ReferenceType.SecurityScheme,
        Id = "ApiKey"
      },
      In = ParameterLocation.Header
    };

    OpenApiSecurityRequirement requirement = new() { { key, new List<string>() } };

    options.AddSecurityRequirement(requirement);

    return options;
  }
}