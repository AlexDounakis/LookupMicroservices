using IPLookup.Configurations;
using IPLookup.Handlers;
using IPLookup.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add configurations
builder.Configuration.AddUserSecrets<Program>();
// Add services to the container.
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection(nameof(ApiSettings)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "IPStack API",
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

builder.Services.AddExceptionHandler<IPServiceNotAvailableExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddRefitClients(builder.Configuration);
builder.Services.AddScoped<IIpLookupService, IpLookupService>();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapSwagger();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
