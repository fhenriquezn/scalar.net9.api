using APIWeaver;
using Asp.Versioning;
using Microsoft.OpenApi.Models;
using scalar.net9.api.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient("dummyjson", client =>
{
    client.BaseAddress = new Uri("https://dummyjson.com/");
});

//Api Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1",options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "SCALAR .NET 9 API v1",
            Version = "1.0.0"
        };

        return Task.CompletedTask;
    });

    // Adds api key security scheme to the api
    options.AddSecurityScheme("X-Api-Key", scheme =>
    {
        scheme.Type = SecuritySchemeType.ApiKey;
        scheme.In = ParameterLocation.Header;
        scheme.Name = "X-Api-Key";
    });
}).AddOpenApi("v2",options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "SCALAR .NET 9 API v2",
            Version = "2.0.0"
        };

        return Task.CompletedTask;
    });

    // Adds api key security scheme to the api
    options.AddSecurityScheme("X-Api-Key", scheme =>
    {
        scheme.Type = SecuritySchemeType.ApiKey;
        scheme.In = ParameterLocation.Header;
        scheme.Name = "X-Api-Key";
    });
});

// Adds api key authentication to the api
builder.Services.AddApiKeyAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
        .WithTitle("SCALAR .NET 9 API")
        .WithTheme(ScalarTheme.Mars)
        .WithSearchHotKey("s")
        .WithModels(false)
        .WithDownloadButton(false)
        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch)
        .WithPreferredScheme("X-Api-Key")
        .WithApiKeyAuthentication(x => x.Token = "5593FA41-884C-443F-8310-F1B3C3D952D3");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
