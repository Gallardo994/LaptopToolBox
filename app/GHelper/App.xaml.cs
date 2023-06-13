using Ninject;
using Ninject.Syntax;

namespace GHelper;

public partial class App : Application
{
    public App(IResolutionRoot kernel)
    {
        InitializeComponent();

        MainPage = (Page) kernel.Get<IStartUpPage>();
    }
}