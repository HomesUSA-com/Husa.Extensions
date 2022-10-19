namespace Husa.Extensions.ServiceBus.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Common.Exceptions;
    using Husa.Extensions.ServiceBus.Extensions;
    using Husa.Extensions.ServiceBus.Handlers;
    using Husa.Extensions.ServiceBus.Subscribers;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public abstract class MessagesHandler<T> : IHandleMessages
        where T : class
    {
        private readonly IProvideSubscriptionClient subscriptionClient;
        private readonly IServiceScopeFactory scopeFactory;

        protected MessagesHandler(IProvideSubscriptionClient subscriptionClient, IServiceScopeFactory scopeFactory, ILogger<T> logger)
        {
            this.subscriptionClient = subscriptionClient ?? throw new ArgumentNullException(nameof(subscriptionClient));
            this.scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected ILogger<T> Logger { get; }

        public async Task HandleMessage(Message message, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation("message received with id: {messageId}", message.MessageId);
            await using (var scope = this.scopeFactory.CreateAsyncScope())
            {
                try
                {
                    await this.ProcessMessageAsync(message, scope, cancellationToken);
                }
                catch (NotFoundException notFoundException)
                {
                    this.Logger.LogError(notFoundException, "Received not found exception when processing message {messageId}, moving to dead letter queue...", message.MessageId);
                    await this.subscriptionClient.Client.DeadLetterAsync(message.GetLockToken());
                    return;
                }
                catch (DomainException domainException)
                {
                    this.Logger.LogWarning(domainException, "Received domain exception error when processing message {messageId}, skipping retry...", message.MessageId);
                }
            }

            await this.subscriptionClient.Client.CompleteAsync(message.GetLockToken());
        }

        public Task HandleException(ExceptionReceivedEventArgs exceptionReceived)
        {
            if (exceptionReceived is null)
            {
                throw new ArgumentNullException(nameof(exceptionReceived));
            }

            this.Logger.LogError(exceptionReceived.Exception, "Received exception when processing a message {message}", exceptionReceived.Exception.Message);
            return Task.FromException(exceptionReceived.Exception);
        }

        public abstract Task ProcessMessageAsync(Message message, IServiceScope scope, CancellationToken cancellationToken);
    }
}
