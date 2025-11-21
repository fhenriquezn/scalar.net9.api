using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

internal sealed class AddResponsesOperationTransformer : IOpenApiOperationTransformer
{
    private static readonly IDictionary<string, OpenApiResponse> _responses = new Dictionary<string, OpenApiResponse>(StringComparer.Ordinal)
    {
        ["200"] = new OpenApiResponse { Description = "OK" },
        ["201"] = new OpenApiResponse { Description = "Created" },
        ["400"] = new OpenApiResponse { Description = "Bad Request" },
        ["401"] = new OpenApiResponse { Description = "Unauthorized" },
        ["403"] = new OpenApiResponse { Description = "Forbidden" },
        ["404"] = new OpenApiResponse { Description = "Not Found" },
        ["500"] = new OpenApiResponse { Description = "Internal Server Error" }
    };

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        operation.Responses ??= [];

        foreach (var kv in _responses)
        {
            if (!operation.Responses.ContainsKey(kv.Key))
            {
                operation.Responses.Add(kv.Key, kv.Value);
            }
        }

        return Task.CompletedTask;
    }
}