using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GHelper.Web;

public class PooledHttpClient : IHttpClient
{
    private readonly HttpClient _httpClient;
    
    private readonly IHttpClientFactory _httpClientFactory;

    public PooledHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _httpClient = new HttpClient();
    }
    
    public IHttpClient WithUserAgent(ProductInfoHeaderValue userAgent)
    {
        _httpClient.DefaultRequestHeaders.UserAgent.Clear();
        _httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
        
        return this;
    }
    
    public IHttpClient WithHeader(string name, string value)
    {
        _httpClient.DefaultRequestHeaders.Add(name, value);
        
        return this;
    }
    
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return _httpClient.SendAsync(request);
    }
    
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _httpClient.SendAsync(request, cancellationToken);
    }
    
    private void Reset()
    {
        _httpClient.DefaultRequestHeaders.Clear();
    }

    public void Dispose()
    {
        Reset();
        
        _httpClientFactory.Return(this);
    }
}