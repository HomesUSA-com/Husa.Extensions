namespace Husa.Extensions.Quickbooks.Models.Invoice.AttachResponse
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class AttachableObjectResponse
    {
        public int Status { get; set; }

        public Dictionary<string, string> SectionSelector { get; set; }

        public object Data { get; set; }

        public IntuitResponse ParseDataToAttachable()
        {
            string jsonData = this.Data.ToString();
            return JsonConvert.DeserializeObject<IntuitResponse>(jsonData);
        }
    }
}
