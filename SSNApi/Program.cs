using SSNApi.Domain;

using SSNLib;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
  _ = builder.WebHost.UseSetting("http_port", "80");
  _ = builder.WebHost.UseSetting("https_port", "443");
}

builder.Services
  .AddServices()
  .AddDomain()
  .AddAuthorization()
  .AddControllers();
  
builder.Services.AddEndpointsApiExplorer()
  .AddSwaggerGen(c => c.AddSwaggerGenOptions());

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
}
_ = app.UseSwagger();
_ = app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
