using Ninject.Modules;

namespace GHelper.DrawingHelpers;

public class Bindings : NinjectModule
{
    public override void Load()
    {
        Bind<IDrawingHelper>().To<DrawingHelper>();
    }
}