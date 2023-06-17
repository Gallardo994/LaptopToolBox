using System.Collections.Generic;
using System.Threading.Tasks;
using GHelper.Updates.Models;

namespace GHelper.Updates.Core;

public interface IUpdatesChecker
{
    public Task<List<IUpdate>> CheckForUpdates();
}