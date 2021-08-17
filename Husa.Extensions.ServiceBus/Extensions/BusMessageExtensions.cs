namespace Husa.Extensions.ServiceBus.Extensions
{
    using Microsoft.Azure.ServiceBus;

    public static class BusMessageExtensions
    {
        public static T DeserializeMessage<T>(this Message message)
            where T : class
        {
            return message.Body.DeserializeMessage<T>();
        }

        public static string GetLockToken(this Message message)
        {
            // msg.SystemProperties.LockToken Get property throws exception if not set. Return null instead.
            return message.SystemProperties.IsLockTokenSet ? message.SystemProperties.LockToken : null;
        }
    }
}
