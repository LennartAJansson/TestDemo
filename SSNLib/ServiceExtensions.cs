namespace SSNLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using SSNApi.Domain.Interfaces;

public static class ServiceExtensions
{
  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<ISSNServices, SSNServices>();
    
    return services;
  }
}


