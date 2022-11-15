namespace Husa.Extensions.Downloader.RetsSdk.Services
{
    using Husa.Extensions.Downloader.RetsSdk.Models;
    using System.Threading.Tasks;

    public interface IRetsSession
    {
        Task<bool> Start();
        Task End();
        int GetMarketLimit();

        SessionResource Resource { get; }
        bool IsStarted();
    }
}
