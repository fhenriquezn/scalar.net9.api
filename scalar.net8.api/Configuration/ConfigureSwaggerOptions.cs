using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace scalar.net8.api.Configuration
{
    /// <summary>
    /// Configures the Swagger options for the API.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </remarks>
    /// <param name="provider">The API version description provider.</param>
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider = provider;

        /// <inheritdoc/>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = $"{description.ApiVersion}",
                Title = $"Scalar .NET 8 Title API",
                Description = "An ASP.NET Core Web API",
            };
            return info;
        }
    }
}
