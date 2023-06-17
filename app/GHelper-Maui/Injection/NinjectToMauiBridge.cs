using Ninject.Syntax;

namespace GHelper.Injection;

public static class NinjectToMauiBridge
{
    public static MauiAppBuilder UseNinject(this MauiAppBuilder builder, IResolutionRoot root)
    {
        builder.Services.AddSingleton(root);
        return builder;
    }
}