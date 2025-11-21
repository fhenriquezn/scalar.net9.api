using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi;
using scalar.net10.api.Handlers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
});

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
var v1Info = new OpenApiInfo
{
    Title = $"SCALAR .NET 10 API v1 - {builder.Environment.EnvironmentName}",
    Version = "1.0.0"
};

var v2Info = new OpenApiInfo
{
    Title = $"SCALAR .NET 10 API v2 - {builder.Environment.EnvironmentName}",
    Version = "2.0.0"
};

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1", options =>
{
    // assign the v1 OpenApiInfo to the generated document
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = v1Info;
        return Task.CompletedTask;
    });

    // shared transformer that injects the API key scheme and other components
    options.AddDocumentTransformer<SecuritySchemeTransformer>();
    // shared transformer that adds common responses to all operations
    options.AddOperationTransformer<AddResponsesOperationTransformer>();
})
.AddOpenApi("v2", options =>
{
    // assign the v2 OpenApiInfo to the generated document
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = v2Info;
        return Task.CompletedTask;
    });

    options.AddDocumentTransformer<SecuritySchemeTransformer>();
    options.AddOperationTransformer<AddResponsesOperationTransformer>();
});

builder.Services.AddAuthentication("ApiKeyScheme")
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationSchemeHandler>("ApiKeyScheme", null);
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var documents = new[]
{
    new ScalarDocument("v1", "API v1"), //openapi/v1.json
    new ScalarDocument("v2", "API v2") //openapi/v2.json
};
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Servers = [];
        options
        .WithTitle("SCALAR .NET 10 API")
        .WithTheme(ScalarTheme.Mars)
        .WithSearchHotKey("s")
        .HideModels()
        .WithDocumentDownloadType(DocumentDownloadType.None)
        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch)
        .AddPreferredSecuritySchemes("X-Api-Key")
        .AddApiKeyAuthentication("X-Api-Key", scheme =>
        {
            scheme.Name = "X-Api-Key";
            scheme.Value = "5593FA41-884C-443F-8310-F1B3C3D952D3";
        });
        options.AddDocuments(documents);
    });
}

app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();