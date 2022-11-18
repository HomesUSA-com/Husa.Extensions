namespace Husa.Extensions.Authorization.Extensions
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Husa.Extensions.Authorization.Enums;
    using Husa.Extensions.Authorization.Models;

    public static class UserExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var nameIdentifier = user.FindFirst(ClaimTypes.NameIdentifier);
            return nameIdentifier != null ? new Guid(nameIdentifier.Value) : Guid.Empty;
        }

        public static IUserContext GetUserContext(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var nameIdentifier = user.FindFirst(ClaimTypes.NameIdentifier);

            var usernameClaim = user.Claims.FirstOrDefault(x => x.Type == HusaClaimTypes.Username);
            if (usernameClaim is null)
            {
                throw new InvalidOperationException($"{HusaClaimTypes.Username} claim must be informed");
            }

            var username = usernameClaim.Value.ToString();
            return new UserContext
            {
                Email = username,
                Id = nameIdentifier != null ? new Guid(nameIdentifier.Value) : Guid.Empty,
                Name = username,
                IsMLSAdministrator = user.IsMLSAdministrator(),
                UserRole = user.GetUserRole(),
            };
        }

        public static UserRole GetUserRole(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roles = user.FindAll(ClaimTypes.Role);

            if (Enum.TryParse(roles.First().Value, out UserRole userRole))
            {
                return userRole;
            }

            return UserRole.User;
        }

        public static bool IsMLSAdministrator(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roles = user.FindAll(ClaimTypes.Role);
            return roles.Any(role => role.Value == "MLSAdministrator");
        }
    }
}
