using System;

namespace GHelper.Web.Models;

public class HttpDownloadMessage
{
    public Uri Uri { get; internal set; }
    public string SavePath { get; internal set; }
    public long TotalSize { get; internal set; }
    public HttpDownloadMessageStatus Status { get; internal set; }
    public long BytesReceived { get; internal set; }
    public double PercentComplete => TotalSize > 0 ? (double) BytesReceived / TotalSize : 0;

    public enum HttpDownloadMessageStatus
    {
        Unknown,
        Downloading,
        Completed,
        Failed,
        Cancelled,
    }
}