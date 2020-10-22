using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FFImageLoading;
using FFImageLoading.Cache;
using FFImageLoading.Config;
using FFImageLoading.Exceptions;
using FFImageLoading.Work;

namespace AtWork.Services
{
    public class CustomDownloadCacheService : DownloadCache
    {
        public CustomDownloadCacheService(Configuration configuration) : base(configuration)
        {
        }

        protected override async Task<byte[]> DownloadAsync(string url, CancellationToken token,
            HttpClient client, TaskParameter parameters, DownloadInformation downloadInformation)
        {
            try
            {
                return await base.DownloadAsync(url, token, client, parameters, downloadInformation);
            }
            catch (Exception e) when (e is DownloadHeadersTimeoutException ex)
            {
                throw new System.OperationCanceledException("Download headers timeout", ex);
            }
        }
    }
}
