using GHelper.Localization;
using Ninject;

namespace GHelper.Core;

public class MainCoreRunner : ICoreRunner
{
    private readonly ILanguageSetter _languageSetter;
    
    [Inject]
    public MainCoreRunner(ILanguageSetter languageSetter)
    {
        _languageSetter = languageSetter;
    }

    public void Run(string[] args)
    {
        _languageSetter.ResolveAndSetLanguage();
        
        ProcessHelper.CheckAlreadyRunning();
    }
}