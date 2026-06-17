using System.Security.Claims;

namespace API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long? GetUserId(this ClaimsPrincipal user)
    {
        var idValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return long.TryParse(idValue, out long id) ? id : null;
    }
}