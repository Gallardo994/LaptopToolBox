using System.Threading.Tasks;

namespace LaptopToolBox;

public interface IPageBackHandler
{
    public Task<bool> TryHandleBackAsync();
}