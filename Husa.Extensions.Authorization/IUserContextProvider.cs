namespace Husa.Extensions.Authorization
{
    using System;
    public interface IUserContextProvider
    {
        Guid GetCurrentUserId();

        IUserContext GetCurrentUser();

        bool HasCurrentUser();
    }
}
