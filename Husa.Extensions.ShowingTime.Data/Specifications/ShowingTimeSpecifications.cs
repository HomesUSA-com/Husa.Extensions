namespace Husa.Extensions.ShowingTime.Data.Specifications
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Husa.Extensions.Domain.Entities;
    using Husa.Extensions.ShowingTime.Interfaces;

    [ExcludeFromCodeCoverage]
    public static class ShowingTimeSpecifications
    {
        public static IQueryable<T> ApplyShowingSearchByFilter<T>(this IQueryable<T> query, string searchByFilter)
            where T : Entity, IShowingTimeContact
        {
            if (string.IsNullOrEmpty(searchByFilter))
            {
                return query;
            }

            return query.Where(x => x.FirstName.Contains(searchByFilter)
                || x.LastName.Contains(searchByFilter)
                || x.Email.Contains(searchByFilter)
                || x.MobilePhone.Contains(searchByFilter)
                || x.OfficePhone.Contains(searchByFilter));
        }
    }
}
