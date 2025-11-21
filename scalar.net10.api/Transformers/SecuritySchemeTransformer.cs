using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

internal sealed class SecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider, IWebHostEnvironment env) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (!authenticationSchemes.Any(authScheme => authScheme.Name == "ApiKeyScheme"))
            return;

        // Define the API key security scheme (document-level)
        var securitySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["X-Api-Key"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-Api-Key",
                Description = "API key needed to access the endpoints. Provide in the X-Api-Key request header."
            }
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = securitySchemes;

        // Apply the scheme as a requirement to every operation in the document
        if (document.Paths is not null)
        {
            foreach (var pathItem in document.Paths.Values)
            {
                if (pathItem?.Operations == null)
                    continue;

                foreach (var operation in pathItem.Operations.Values)
                {
                    operation.Security ??= [];
                    operation.Security.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("X-Api-Key", document)] = []
                    });
                }
            }
        }

        // Ensure reusable responses container exists (use concrete OpenApiResponse per docs)
        document.Components.Responses ??= new OpenApiResponses();

        // Do not overwrite document.Info if already assigned by a per-document transformer
        document.Info ??= new OpenApiInfo
        {
            Title = $"SCALAR .NET 10 API v1 - {env.EnvironmentName}",
            Version = "1.0.0"
        };
    }
}