using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace VacationHire.Authentication.Auth;

/// <summary>
///     Basic authentication - Authorize attribute is used to protect the controller
///     - If a request requires authentication, the server returns 401 (Unauthorized)  if not authenticated
///     - The client sends the Authorization header with the credentials (username and password) encoded in Base64 format
///
///     - After the credentials are extracted they are validated against a user store (database, external service, ...);
/// In this demo application, the credential validation uses hardcoded values: username = "user@vacationhire.com"; password = "DummyPassword"
/// </summary>
/// <param name="options">Gets or sets the options associated with this authentication handler</param>
/// <param name="logger"></param>
/// <param name="encoder"></param>
public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Unauthorized");

        string authorizationHeader = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
            return AuthenticateResult.Fail("Unauthorized");

        if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.Fail("Unauthorized");

        var token = authorizationHeader.Substring(6);
        var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

        var credentials = credentialAsString.Split(":");
        if (credentials.Length != 2)
            return AuthenticateResult.Fail("Unauthorized");

        var username = credentials[0];
        var password = credentials[1];

        // Hard coded values
        if (username != "user@vacationhire.com" && password != "DummyPassword")
            return AuthenticateResult.Fail("Authentication failed");


        // For authenticated users a new claim is added: NameIdentifier: unique identifier for the user
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username)
        };
        var identity = new ClaimsIdentity(claims, "Basic");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
    }
}