namespace Husa.Extensions.Downloader.RetsSdk.Models
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Xml.Linq;

    [Description("METADATA-OBJECT")]
    public class RetsObjectCollection : RetsCollection<RetsObject>
    {
        public override void Load(XElement xElement)
        {
            Load(GetType(), xElement);
        }

        public override RetsObject Get(object value)
        {
            RetsObject item = Get().FirstOrDefault(x => x.ObjectType.Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase));

            return item;
        }
    }
}
