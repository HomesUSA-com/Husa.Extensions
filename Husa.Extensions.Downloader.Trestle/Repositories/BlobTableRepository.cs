namespace Husa.Extensions.Downloader.Trestle.Services
{
    using System;
    using System.Threading.Tasks;
    using Azure;
    using Azure.Data.Tables;
    using Husa.Extensions.Downloader.Trestle.Contracts;
    using Husa.Extensions.Downloader.Trestle.Models;
    using Husa.Extensions.Downloader.Trestle.Models.TableEntities;
    using Microsoft.Extensions.Options;

    public class BlobTableRepository : IBlobTableRepository
    {
        private readonly TableServiceClient tableServiceClient;
        private readonly BlobOptions blobOptions;

        public BlobTableRepository(IOptions<BlobOptions> blobOptions)
        {
            this.blobOptions = blobOptions.Value ?? throw new ArgumentNullException(nameof(blobOptions));
            this.tableServiceClient = new TableServiceClient(blobOptions.Value.ConnectionString);
        }

        public async Task<TokenEntity> GetTokenInfoFromStorage(AuthInfo authInfo)
        {
            TableClient tableClient = this.tableServiceClient.GetTableClient(tableName: this.blobOptions.TableName);
            await tableClient.CreateIfNotExistsAsync();
            return await GetTokenInfoFromStorage(tableClient, authInfo);
        }

        public async Task SaveTokenInfo(AuthenticationResult auth, AuthInfo authInfo)
        {
            TableClient tableClient = this.tableServiceClient.GetTableClient(tableName: this.blobOptions.TableName);
            await tableClient.CreateIfNotExistsAsync();

            var tokenEntity = await GetTokenInfoFromStorage(tableClient, authInfo);

            var token = new TokenEntity()
            {
                RowKey = "1",
                PartitionKey = authInfo.PartitionKey,
                AccessToken = auth.AccessToken,
                ExpireDate = DateTimeOffset.Now.AddSeconds(auth.ExpiresIn - 900),
                TokenType = auth.TokenType,
            };

            if (tokenEntity != null)
            {
                await tableClient.UpdateEntityAsync(token, ETag.All, TableUpdateMode.Replace);
            }
            else
            {
                await tableClient.AddEntityAsync(token);
            }
        }

        private static async Task<TokenEntity> GetTokenInfoFromStorage(TableClient tableClient, AuthInfo authInfo)
        {
            try
            {
                var tokenInfo = await tableClient.GetEntityAsync<TokenEntity>(
                rowKey: "1",
                partitionKey: authInfo.PartitionKey);
                return tokenInfo;
            }
            catch (RequestFailedException ex)
            {
                if (ex.ErrorCode == "ResourceNotFound")
                {
                    return null;
                }

                throw;
            }
        }
    }
}
