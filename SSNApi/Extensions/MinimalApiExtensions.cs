using SSNApi.Attributes;

namespace SSNApi.Extensions;

using Microsoft.AspNetCore.Http;

public static class MinimalApiExtensions
{
    private static readonly IEndpointFilter _apiKeyMetadata = new ApiKeyAttribute();
    public static TBuilder UseApiKey<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        _ = builder.AddEndpointFilter(_apiKeyMetadata);

        return builder;
    }
}

