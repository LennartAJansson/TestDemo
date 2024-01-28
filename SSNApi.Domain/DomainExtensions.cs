namespace SSNApi.Domain;

using Microsoft.Extensions.DependencyInjection;

public static class DomainExtensions
{
  public static IServiceCollection AddDomain(this IServiceCollection services)
  {
    _ = services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DomainExtensions).Assembly));

    return services;
  }
}
