

using CRMService.Communicators;
using CRMService.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<CrmDbCommunicator>();
builder.Services.AddSingleton<ApplicationsManager>();
var app = builder.Build();
app.MapGrpcService<CRMService.Services.CRMService>();
// Configure the HTTP request pipeline.
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();