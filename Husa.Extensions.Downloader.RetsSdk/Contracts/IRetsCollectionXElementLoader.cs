namespace Husa.Extensions.Downloader.RetsSdk.Contracts
{
    using System.Xml.Linq;

    public interface IRetsCollectionXElementLoader
    {
        void Load(XElement xElement);
    }
}
