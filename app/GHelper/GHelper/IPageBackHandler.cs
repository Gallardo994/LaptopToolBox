using System.Threading.Tasks;

namespace GHelper;

public interface IPageBackHandler
{
    public Task<bool> TryHandleBackAsync();
}