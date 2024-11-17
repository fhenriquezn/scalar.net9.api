using Microsoft.AspNetCore.Authentication;
using scalar.net9.api.Handlers;

namespace scalar.net9.api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddApiKeyAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("X-Api-Key")
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationSchemeHandler>("X-Api-Key", null);
            services.AddAuthorization();
            return services;
        }
    }
}
