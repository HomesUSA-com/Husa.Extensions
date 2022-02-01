namespace Husa.Extensions.Authorization
{
    using System;
    using Microsoft.Extensions.Logging;

    public class UserProvider : IUserProvider, IUserContextProvider
    {
        private readonly ILogger<UserProvider> logger;
        private IUserContext currentUser;

        public UserProvider(ILogger<UserProvider> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IUserContext GetCurrentUser()
        {
            if (this.currentUser == null)
            {
                throw new InvalidOperationException("The current user is not yet set, Please set it before using it.");
            }

            this.logger.LogInformation($"Returning current user, id: '{this.currentUser.Id}'");
            return this.currentUser;
        }

        public Guid GetCurrentUserId()
        {
            this.logger.LogInformation($"Returning current userId: '{this.currentUser.Id}'");
            return this.currentUser.Id;
        }

        public void SetCurrentUser(IUserContext user)
        {
            this.logger.LogInformation($"Setting current user with id:'{user.Id}'");
            this.currentUser = user;
        }
    }
}
