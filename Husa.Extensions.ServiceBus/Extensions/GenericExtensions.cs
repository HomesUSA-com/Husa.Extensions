namespace Husa.Extensions.ServiceBus.Extensions
{
    using System;
    using System.Text;
    using System.Text.Json;
    using Husa.Extensions.ServiceBus.Interfaces;

    public static class GenericExtensions
    {
        public static T DeserializeMessage<T>(this byte[] message)
            where T : class
        {
            var msg = Encoding.UTF8.GetString(message);
            return JsonSerializer.Deserialize<T>(msg);
        }

        public static byte[] SerializeMessage<T>(this T message)
            where T : IProvideBusEvent
        {
            var data = JsonSerializer.Serialize(message);
            return Encoding.UTF8.GetBytes(data);
        }

        public static object DeserializeMessage(this byte[] message, Type type)
        {
            var msg = Encoding.UTF8.GetString(message);

            return JsonSerializer.Deserialize(msg, type);
        }

        public static object DeserializeMessage(this byte[] message, string assemblyName)
        {
            var type = Type.GetType(assemblyName);
            return message.DeserializeMessage(type);
        }
    }
}
