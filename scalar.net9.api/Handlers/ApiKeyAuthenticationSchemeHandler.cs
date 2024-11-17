using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace scalar.net9.api.Handlers
{
    internal sealed class ApiKeyAuthenticationSchemeHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.TryGetValue("X-Api-Key", out var apiKey))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (apiKey != "5593FA41-884C-443F-8310-F1B3C3D952D3")
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid API key"));
            }

            var identity = new ClaimsIdentity([new Claim(ClaimTypes.Name, "Api-Key-User")], Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }
    }
}
