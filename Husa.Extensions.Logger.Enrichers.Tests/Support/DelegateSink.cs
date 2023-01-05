namespace Husa.Extensions.Logger.Enrichers.Tests.Support
{
    using System;
    using Serilog;
    using Serilog.Core;
    using Serilog.Events;

    public class DelegateSink
    {
        public class DelegatingSink : ILogEventSink
        {
            private readonly Action<LogEvent> write;

            public DelegatingSink(Action<LogEvent> write)
            {
                this.write = write ?? throw new ArgumentNullException(nameof(write));
            }

            public static LogEvent GetLogEvent(Action<ILogger> writeAction)
            {
                LogEvent result = null;
                var l = new LoggerConfiguration()
                  .WriteTo.Sink(new DelegatingSink(le => result = le))
                  .CreateLogger();

                writeAction(l);
                return result;
            }

            public void Emit(LogEvent logEvent)
            {
                this.write(logEvent);
            }
        }
    }
}
