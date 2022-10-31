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

        public bool HasCurrentUser => this.currentUser != null;

        public IUserContext GetCurrentUser()
        {
            if (!this.HasCurrentUser)
            {
                throw new InvalidOperationException("The current user is not yet set, Please set it before using it.");
            }

            this.logger.LogInformation("Returning current user '{userId}'", this.currentUser.Id);
            return this.currentUser;
        }

        public Guid GetCurrentUserId()
        {
            if (!this.HasCurrentUser)
            {
                this.logger.LogWarning("Current user is not set, returning '{userId}'", Guid.Empty);
                return Guid.Empty;
            }

            this.logger.LogInformation("Returning current user Id '{userId}'", this.currentUser.Id);
            return this.currentUser.Id;
        }

        public void SetCurrentUser(IUserContext user)
        {
            this.logger.LogInformation("Setting current user '{userId}'", user.Id);
            this.currentUser = user;
        }
    }
}
