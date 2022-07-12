namespace Husa.Extensions.EmailNotification.Configuration.Settings
{
    using System.Collections.Generic;
    using Husa.Extensions.EmailNotification.Enums;

    public class EmailTemplate
    {
        public TemplateType TemplateType { get; set; }

        public string Subject { get; set; }

        public IEnumerable<EmailParameter> Parameters { get; set; }
    }
}
