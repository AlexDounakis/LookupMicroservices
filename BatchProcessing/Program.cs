using AutoMapper;
using BatchProcessing.Exceptions;
using BatchProcessing.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using IpDetailsCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Batch Processing API",
        Version = "v1",
        Description = "no description",
        Contact = new OpenApiContact()
        {
            Name = "Alex",
            Email = "your@email.com",
        }

    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddHangfire(x => x.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddMemoryCache();

builder.Services.AddExceptionHandler<IPServiceNotAvailableExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddCacheConfig();
builder.Services.AddSingleton<IBatchService, BatchService>();
builder.Services.AddSingleton<IJobProcessor, JobProcessor>();
builder.Services.AddRefitClients(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
