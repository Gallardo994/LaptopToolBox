namespace GHelper.Web;

public interface IHttpClientFactory
{
    public IHttpClient Get();
    internal void Return(IHttpClient httpClient);
}