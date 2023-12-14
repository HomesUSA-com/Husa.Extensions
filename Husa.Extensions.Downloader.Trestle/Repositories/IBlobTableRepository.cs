namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System.Threading.Tasks;
    using Husa.Extensions.Downloader.Trestle.Contracts;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Husa.Extensions.Downloader.Trestle.Models.TableEntities;

    public interface IBlobTableRepository
    {
        Task<TokenEntity> GetTokenInfoFromStorage(AuthInfo authInfo);
        Task SaveTokenInfo(AuthenticationResult auth, AuthInfo authInfo);
    }
}
