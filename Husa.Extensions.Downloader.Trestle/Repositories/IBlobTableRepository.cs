namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System.Threading.Tasks;
    using Husa.Extensions.Downloader.Trestle.Contracts;
    using Husa.Extensions.Downloader.Trestle.Models.TableEntities;

    public interface IBlobTableRepository 
    {
        Task<TokenEntity> GetTokenInfoFromStorage();
        Task SaveTokenInfo(AuthenticationResult auth);
    }
}
