using Ninject.Syntax;

namespace LaptopToolBox.Injection;

public static class Services
{
    public static IResolutionRoot ResolutionRoot { get; set; } = null!;
}