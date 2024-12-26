using Asp.Versioning;
using scalar.net8.api.Extensions;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Api Versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = ApiVersion.Default;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(option =>
{
    option.GroupNameFormat = "'v'VVV";
    option.SubstituteApiVersionInUrl = true;
});

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var xmlFilename = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
builder.Services.SetupSwaggerGenConfig(xmlFilename);

// Adds api key authentication to the api
builder.Services.AddApiKeyAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";

    });

    app.MapScalarApiReference(options =>
    {
        options
        .WithTitle("Scalar Api Title")
        .WithTheme(ScalarTheme.DeepSpace)
        .WithSearchHotKey("s")
        .WithModels(false)
        .WithDownloadButton(false)
        .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Fetch)
        .WithPreferredScheme("ApiKey")
        .WithApiKeyAuthentication(x => x.Token = "5593FA41-884C-443F-8310-F1B3C3D952D3");
    });
}
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
