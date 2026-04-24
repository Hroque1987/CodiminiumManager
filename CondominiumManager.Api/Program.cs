using CondominiumManager.Api.DevUtilities;
using CondominiumManager.Api.Middleware;
using CondominiumManager.Condominium;
using CondominiumManager.Finance;
using CondominiumManager.Identity;
using CondominiumManager.Notifications;
using FastEndpoints;
using InfraStructure;
using Scalar.AspNetCore;
using Serilog;
using Sharedkernel.DomainEvents;

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting Web Server");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddFastEndpoints();

builder.Services.AddCondominium(builder.Configuration, logger);
builder.Services.AddFinance(builder.Configuration, logger);
builder.Services.AddIdentity(builder.Configuration, logger);
builder.Services.AddNotifications(builder.Configuration, logger);

builder.Services.AddInfraStructure();

builder.Services.AddScoped<IDomainEventHandler<UserRegisteredEvent>, SendWelcomeEmailHandler>(); //Just For Testing


builder.Services.AddOpenApi();



var app = builder.Build();



app.UseCorrelation();

app.UseExceptionHandler();

app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}



app.UseHttpsRedirection();

app.Run();



