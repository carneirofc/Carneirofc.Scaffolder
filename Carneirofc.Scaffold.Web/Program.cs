using Carneirofc.Scaffold.Web.Conventions;
using Carneirofc.Scaffold.Web.Extensions;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomLogging();
builder.AddConfiguration(args);

// Add feature management to the container of services.
builder.Services.AddFeatureManagement();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseControllerModelConvention());
});
builder.Services.AddHttpClient();

builder.Services.AddCustomInfrastructureServices();
builder.Services.AddCustomApplicationServices();

builder.Services.AddCustomHealthChecks();
builder.Services.AddCustomSwagger();


var app = builder.Build();

// Configure the HTTP request pipeline, middleware, and endpoints.
app.Configure();

app.Run();
