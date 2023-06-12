using System.Security.Principal;

namespace GHelper.ProcessHelpers;

public class AdministratorHelper : IAdministratorHelper
{
    public bool IsUserAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}