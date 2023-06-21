using System.Collections.ObjectModel;

namespace GHelper.About;

public class AboutProvider : IAboutProvider
{
    public ObservableCollection<IAboutItem> Items { get; }
    
    public AboutProvider()
    {
        Items = new ObservableCollection<IAboutItem>
        {
            new AboutItem
            {
                Title = "GHelper",
                Description = "Lightweight Armoury Crate alternative for Asus laptops",
                Link = "https://github.com/seerge/g-helper",
                LicenseLink = "https://github.com/seerge/g-helper/blob/main/LICENSE",
            },
            new AboutItem
            {
                Title = "WinRing0",
                Description = "WinRing0 is a hardware access library for Windows",
                Link = "https://github.com/GermanAizek/WinRing0",
                LicenseLink = "https://github.com/GermanAizek/WinRing0/blob/master/LICENSE",
            },
            new AboutItem
            {
                Title = "NvAPIWrapper",
                Description = "NvAPIWrapper is a .Net wrapper for NVIDIA public API, capable of managing all aspects of a display setup using NVIDIA GPUs",
                Link = "https://github.com/falahati/NvAPIWrapper",
                LicenseLink = "https://github.com/falahati/NvAPIWrapper/blob/master/LICENSE",
            },
            new AboutItem
            {
                Title = "hidlibrary",
                Description = "This library enables you to enumerate and communicate with Hid compatible USB devices in .NET",
                Link = "https://github.com/mikeobrien/HidLibrary",
                LicenseLink = "https://github.com/mikeobrien/HidLibrary/blob/master/LICENSE",
            },
            new AboutItem
            {
                Title = "Serilog",
                Description = "Serilog is a diagnostic logging library for .NET applications",
                Link = "https://github.com/serilog/serilog",
                LicenseLink = "https://github.com/serilog/serilog/blob/main/LICENSE",
            },
            new AboutItem
            {
                Title = "Ninject",
                Description = "Ninject is a lightning-fast, ultra-lightweight dependency injector for .NET applications",
                Link = "https://github.com/ninject/Ninject",
                LicenseLink = "https://github.com/ninject/Ninject/blob/main/LICENSE.txt",
            },
            new AboutItem
            {
                Title = "Newtonsoft.Json",
                Description = "Popular high-performance JSON framework for .NET",
                Link = "https://github.com/JamesNK/Newtonsoft.Json",
                LicenseLink = "https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md",
            },
            new AboutItem
            {
                Title = "WindowsAppSDK",
                Description = "Windows App SDK is a set of libraries, frameworks, components, and tools for Windows platform",
                Link = "https://github.com/microsoft/windowsappsdk",
                LicenseLink = "https://github.com/microsoft/WindowsAppSDK/blob/main/LICENSE",
            }
        };
    }
}