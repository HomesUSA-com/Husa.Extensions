namespace Husa.Extensions.EmailNotification.Configuration.Settings
{
    using System;
    using System.Collections.Generic;

    public class EmailTemplate<TTemplateType>
        where TTemplateType : struct, Enum
    {
        public TTemplateType TemplateType { get; set; }

        public string Subject { get; set; }

        public IEnumerable<string> Parameters { get; set; }
    }
}
