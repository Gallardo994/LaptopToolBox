using System.Collections.Generic;
using System.Threading.Tasks;
using LaptopToolBox.Updates.Models;

namespace LaptopToolBox.Updates.Core;

public interface IUpdatesChecker
{
    public Task<List<IUpdate>> CheckForUpdates();
}