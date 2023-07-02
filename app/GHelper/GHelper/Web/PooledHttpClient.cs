using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GHelper.Web.Models;
using Newtonsoft.Json;

namespace GHelper.Web;

public class PooledHttpClient : IHttpClient
{
    private readonly HttpClient _httpClient;
    
    private readonly IHttpClientFactory _httpClientFactory;

    public PooledHttpClient(IHttpClientFactory httpClientFactory, ProductInfoHeaderValue userAgent)
    {
        _httpClientFactory = httpClientFactory;

        _httpClient = new HttpClient
        {
            DefaultRequestHeaders =
            {
                UserAgent =
                {
                    userAgent,
                },
            }
        };
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return _httpClient.SendAsync(request);
    }
    
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _httpClient.SendAsync(request, cancellationToken);
    }
    
    public async Task<HttpDownloadMessage> DownloadFileAsync(Uri uri, string filePath, IProgress<double> progress = null, CancellationToken cancellationToken = default)
    {
        var message = new HttpDownloadMessage
        {
            Uri = uri,
            SavePath = filePath,
            Status = HttpDownloadMessage.HttpDownloadMessageStatus.Unknown,
        };

        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        using var content = response.Content;
        
        var totalBytes = content.Headers.ContentLength;
        var canReportProgress = totalBytes.HasValue && progress != null;
        
        message.TotalSize = totalBytes ?? 0;
        
        var buffer = new byte[8 * 1024];

        try
        {
            await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, buffer.Length, true);
            await using var stream = await content.ReadAsStreamAsync(cancellationToken);

            var isMoreToRead = true;
            
            message.Status = HttpDownloadMessage.HttpDownloadMessageStatus.Downloading;

            do
            {
                var read = await stream.ReadAsync(buffer, cancellationToken);

                if (read == 0)
                {
                    isMoreToRead = false;
                }
                else
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, read), cancellationToken);

                    message.BytesReceived += read;

                    if (canReportProgress)
                    {
                        progress.Report(message.PercentComplete);
                    }
                }
            } while (isMoreToRead);
            
            message.Status = HttpDownloadMessage.HttpDownloadMessageStatus.Completed;
        }
        catch (OperationCanceledException)
        {
            message.Status = HttpDownloadMessage.HttpDownloadMessageStatus.Cancelled;
        }
        catch (Exception)
        {
            message.Status = HttpDownloadMessage.HttpDownloadMessageStatus.Failed;
            throw;
        }

        return message;
    }
    
    public async Task<T> ReadAsJsonAsync<T>(HttpMethod httpMethod, Uri uri)
    {
        var request = new HttpRequestMessage(httpMethod, uri);
        var response = await _httpClient.SendAsync(request);
        
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public void Dispose()
    {
        _httpClientFactory.Return(this);
    }
}