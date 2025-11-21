using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using scalar.net8.api.Configuration;
using scalar.net8.api.Handlers;

namespace scalar.net8.api.Extensions
{
    public static class StartupExtensions
    {
        internal static IServiceCollection AddApiKeyAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("X-Api-Key")
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationSchemeHandler>("X-Api-Key", null);
            services.AddAuthorization();
            return services;
        }

        public static void SetupSwaggerGenConfig(this IServiceCollection services, string? xmlFilename = null)
        {
            var assembly = services.GetType();
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("X-Api-Key", new OpenApiSecurityScheme
                {
                    Name = "X-Api-Key",
                    In = ParameterLocation.Header,
                    Description = "X-Api-Key",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "X-Api-Key"
                });

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "X-Api-Key"
                    },
                    In = ParameterLocation.Header,
                };

                var requirement = new OpenApiSecurityRequirement
                {
                    { scheme, new List<string>() }
                };
                options.AddSecurityRequirement(requirement);
                if (xmlFilename is not null)
                    options.IncludeXmlComments(xmlFilename);

            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}
