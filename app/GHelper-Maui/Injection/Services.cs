using Ninject.Syntax;

namespace GHelper.Injection;

public static class Services
{
    public static IResolutionRoot ResolutionRoot { get; set; } = null!;
}