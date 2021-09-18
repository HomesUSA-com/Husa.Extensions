namespace Husa.Extensions.ServiceBus.Extensions
{
    using System;
    using System.Linq;
    using Microsoft.Azure.ServiceBus;

    public static class BusMessageExtensions
    {
        public static T DeserializeMessage<T>(this Message message)
            where T : class
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return message.Body.DeserializeMessage<T>();
        }

        public static object DeserializeMessage(this Message message, string fullyQualifiedName)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(fullyQualifiedName))
            {
                throw new ArgumentNullException(nameof(fullyQualifiedName), $"The {nameof(fullyQualifiedName)} must be present to deserialize.");
            }

            return message.Body.DeserializeMessage(fullyQualifiedName);
        }

        public static object DeserializeMessage(this Message message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            const string bodyType = "BodyType";
            if (!message.UserProperties.Any() && !message.UserProperties.ContainsKey(bodyType))
            {
                throw new InvalidOperationException($"The key '{bodyType}' must be present to deserialize.");
            }

            return message.DeserializeMessage(message.UserProperties[bodyType].ToString());
        }

        public static object DeserializeMessage(this Message message, Type type)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return message.Body.DeserializeMessage(type);
        }

        public static string GetLockToken(this Message message)
        {
            // msg.SystemProperties.LockToken Get property throws exception if not set. Return null instead.
            return message.SystemProperties.IsLockTokenSet ? message.SystemProperties.LockToken : null;
        }
    }
}
