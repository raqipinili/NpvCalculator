using System;
using System.Linq;
using System.Security.Claims;

namespace NpvCalculator.Security.Helpers.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            Claim nameIdentifier = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (nameIdentifier == null)
                throw new Exception($"Error in GetUserId: Cannot find ClaimType: 'NameIdentifier'");

            return new Guid(nameIdentifier.Value);
        }
    }
}
