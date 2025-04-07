namespace Husa.Extensions.Authorization.Specifications
{
    using System;
    using System.Linq;
    using Husa.Extensions.Authorization.Enums;

    public static class EntitySpecifications
    {
        public static IQueryable<T> FilterByCompany<T>(this IQueryable<T> query, Guid? companyId, bool applyFilter = true)
            where T : IProvideCompany
        {
            if (applyFilter && (!companyId.HasValue || companyId.Value.Equals(Guid.Empty)))
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return applyFilter ? query.Where(p => p.CompanyId.Equals(companyId)) : query;
        }

        public static IQueryable<T> FilterByCompany<T>(this IQueryable<T> query, IUserContext userContext)
            where T : IProvideCompany
        {
            return query.FilterByCompany(userContext.CompanyId, userContext.UserRole == UserRole.User || userContext.CompanyId.HasValue);
        }
    }
}
