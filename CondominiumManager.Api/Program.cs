using CondominiumManager.Condominium;
using CondominiumManager.Finance;
using CondominiumManager.Identity;
using CondominiumManager.Notifications;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.AddCondominium(builder.Configuration);
builder.Services.AddFinance(builder.Configuration);
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddNotifications(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();


