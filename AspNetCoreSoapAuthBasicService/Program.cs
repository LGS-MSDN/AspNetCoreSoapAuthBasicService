using AspNetCoreSoapAuthBasicService;
using AspNetCoreSoapAuthBasicService.Middlewares;
using AspNetCoreSoapAuthBasicService.SoapServices;
using SoapCore;
using SoapCore.Extensibility;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IFaultExceptionTransformer, SoapCoreFaultExceptionTransformer>();
builder.Services.AddSingleton<IBasicAuthDemoSoapService, BasicAuthDemoSoapService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<ValidateSoapRequestMiddleware>();
//app.UseMiddleware<CustomSoapMiddleware>();

app.UseSoapEndpoint<IBasicAuthDemoSoapService>("/BasicAuthDemoSoapService.asmx", new SoapEncoderOptions());

app.Run();
