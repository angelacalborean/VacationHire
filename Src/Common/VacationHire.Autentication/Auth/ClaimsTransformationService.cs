using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using VacationHire.Authentication.Interfaces;

namespace VacationHire.Authentication.Auth;

/// <summary>
///     https://learn.microsoft.com/en-us/aspnet/core/security/authentication/claims?view=aspnetcore-8.0#extend-or-add-custom-claims-using-iclaimstransformation
/// </summary>
public class ClaimsTransformationService : IClaimsTransformation
{
    private readonly IUserService _userService;

    public ClaimsTransformationService(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
            return principal;

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier); // get the value from the claim
        if (userId == null)
            return principal;

        // Use the fake user service to retrieve the user roles
        var roles = await _userService.GetUserRoles(userId.Value);
        if (roles.Count == 0)
            return principal; // no roles found, return

        foreach (var role in roles)
        {
            // Documentation states: his method might get called multiple times. 
            // Only add a new claim if it does not already exist in the ClaimsPrincipal.
            if (principal.HasClaim(ClaimTypes.Role, role))
                continue;

            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return principal;
    }
}