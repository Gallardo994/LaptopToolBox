using Ninject;
using Ninject.Syntax;

namespace GHelper;

public partial class App
{
    public App(IResolutionRoot kernel)
    {
        InitializeComponent();

        MainPage = (Page) kernel.Get<IStartUpPage>();
    }
}