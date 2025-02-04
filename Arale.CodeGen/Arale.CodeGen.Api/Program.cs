using System.Reflection;
using Arale.CodeGen.Api.Filters;
using Arale.CodeGen.Api.Handlers;
using Arale.CodeGen.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddServicesFromAssembly(Assembly.Load("Arale.CodeGen.Services"));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// disable default model state validation
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddControllers(opt => { opt.Filters.Add<RequestParamValidationFilter>(); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction()) app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });
app.UseCors(configurePolicy =>
{
    configurePolicy.AllowCredentials();
    configurePolicy.AllowAnyHeader();
    configurePolicy.AllowAnyMethod();
    // TODO config by env (dev / production?), current for dev
    configurePolicy.WithOrigins("http://localhost:9000", "http://127.0.0.1:9000");
});
app.MapControllers();

await app.RunAsync();
