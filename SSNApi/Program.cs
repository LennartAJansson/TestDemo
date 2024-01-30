using SSNApi.Domain;

using SSNLib;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add CORS to enable all origins
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()));

//From NET8.0 they started to use port 8080 and 8081 as default ports for http and https
if (builder.Environment.IsProduction())
{
  _ = builder.WebHost.UseSetting("http_port", "80");
  _ = builder.WebHost.UseSetting("https_port", "443");
}

// Add our stuff as well as default behavior for authorization and controllers
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

//TODO Removed https redirection due to lack of certificate in kubernetes
//app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
