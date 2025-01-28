using Carneirofc.Scaffold.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomLogging();

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddCustomInfrastructureServices();
builder.Services.AddCustomApplicationServices();

builder.Services.AddCustomHealthChecks();
builder.Services.AddCustomSwagger();


var app = builder.Build();

// Configure the HTTP request pipeline, middleware, and endpoints.
app.Configure();

app.Run();
