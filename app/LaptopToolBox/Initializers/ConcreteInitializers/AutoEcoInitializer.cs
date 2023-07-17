using LaptopToolBox.AutoEco;
using Ninject;

namespace LaptopToolBox.Initializers.ConcreteInitializers;

public class AutoEcoInitializer : IInitializer
{
    private readonly IAutoEco _autoEco;
    
    [Inject]
    public AutoEcoInitializer(IAutoEco autoEco)
    {
        _autoEco = autoEco;
    }
    
    public void Initialize()
    {
        _autoEco.Start();
    }
}