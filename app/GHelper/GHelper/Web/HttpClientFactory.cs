using System.Collections.Concurrent;
using System.Net.Http.Headers;
using GHelper.AppVersion;

namespace GHelper.Web;

public class HttpClientFactory : IHttpClientFactory
{
    private readonly IAppVersionProvider _appVersionProvider;
    
    private readonly ConcurrentStack<IHttpClient> _httpClientPool;
    
    public HttpClientFactory(IAppVersionProvider appVersionProvider)
    {
        _appVersionProvider = appVersionProvider;
        
        _httpClientPool = new ConcurrentStack<IHttpClient>();
    }

    public IHttpClient Get()
    {
        return _httpClientPool.TryPop(out var httpClient) ? httpClient : 
            new PooledHttpClient(this, new ProductInfoHeaderValue("GHelper", _appVersionProvider.GetCurrentVersion().ToString()));
    }

    void IHttpClientFactory.Return(IHttpClient httpClient)
    {
        _httpClientPool.Push(httpClient);
    }
}