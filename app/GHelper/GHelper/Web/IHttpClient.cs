using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GHelper.Web;

public interface IHttpClient : IDisposable
{
    public IHttpClient WithUserAgent(ProductInfoHeaderValue userAgent);
    public IHttpClient WithHeader(string name, string value);
    
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
}