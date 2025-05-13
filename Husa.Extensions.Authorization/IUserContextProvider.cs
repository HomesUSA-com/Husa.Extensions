namespace Husa.Extensions.Authorization
{
    using System;

    public interface IUserContextProvider
    {
        bool HasCurrentUser { get; }

        Guid GetCurrentUserId();

        IUserContext GetCurrentUser();

        TimeZoneInfo GetUserTimeZone();

        DateTime GetUserLocalDate();
    }
}
