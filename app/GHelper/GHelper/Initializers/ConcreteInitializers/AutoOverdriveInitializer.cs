using GHelper.AutoOverdrive;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

public class AutoOverdriveInitializer : IInitializer
{
    private readonly IAutoOverdrive _autoOverdrive;
    
    [Inject]
    public AutoOverdriveInitializer(IAutoOverdrive autoOverdrive)
    {
        _autoOverdrive = autoOverdrive;
    }
    
    public void Initialize()
    {
        _autoOverdrive.Start();
    }
}