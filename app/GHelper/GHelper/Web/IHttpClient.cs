using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GHelper.Web.Models;

namespace GHelper.Web;

public interface IHttpClient : IDisposable
{
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    
    public Task<HttpDownloadMessage> DownloadFileAsync(Uri uri, string filePath, IProgress<double> progress = null, CancellationToken cancellationToken = default);

    public Task<T> ReadAsJsonAsync<T>(HttpMethod httpMethod, Uri uri);
}