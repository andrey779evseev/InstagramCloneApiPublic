using Api;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseCors(req => req
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.MapControllers()
     .RequireRateLimiting("jwt-token");

//set data for logging
if (Environment.GetEnvironmentVariable("EnabledLogging") == "true")
{
    app.Use(async (context, next) =>
    {
        context.Request.EnableBuffering();
        context.Items["RequestStartTime"] = DateTime.Now;
        await next.Invoke(context);
    });
}

app.Run();