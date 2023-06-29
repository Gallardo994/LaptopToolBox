using System.Diagnostics;
using System.IO.Compression;
using Serilog;

namespace UpdateInstaller;

public static class Program
{
    public static string AppDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GHelper");

    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(AppDataFolder, "updater_log.txt"), rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10, retainedFileTimeLimit: TimeSpan.FromDays(3))
            .MinimumLevel.Debug()
            .CreateLogger();

        var parameters = new Dictionary<string, string>();
        
        foreach (var arg in args)
        {
            if (!arg.StartsWith("--"))
            {
                continue;
            }
            
            var indexOfEqualSign = arg.IndexOf('=');
            if (indexOfEqualSign <= 0)
            {
                continue;
            }
            
            var paramName = arg.Substring(2, indexOfEqualSign - 2);
            var paramValue = arg.Substring(indexOfEqualSign + 1).Trim('\"');
            parameters.Add(paramName, paramValue);
        }
        
        foreach (var (key, value) in parameters)
        {
            Log.Information("{Key}={Value}", key, value);
        }

        if (!(parameters.ContainsKey("zipPath") && parameters.ContainsKey("targetDir")))
        {
            Log.Error("Usage: UpdateInstaller.exe --zipPath=<source zip file> --targetDir=<destination directory> [--runAfter=<executable after extraction>]");
            return;
        }

        var sourceZipFile = parameters["zipPath"];
        var destinationDirectory = parameters["targetDir"];
        var runAfterExtraction = parameters.TryGetValue("runAfter", out var parameter) ? parameter : null;

        if (!File.Exists(sourceZipFile))
        {
            Log.Error("Source zip file does not exist");
            return;
        }

        if (!Directory.Exists(destinationDirectory))
        {
            Log.Error("Destination directory does not exist");
            return;
        }
        
        Log.Information("Waiting for GHelper to exit");
        while (Process.GetProcessesByName("GHelper").Length > 0)
        {
            Thread.Sleep(1000);
        }
        
        var success = false;

        try
        {
            Log.Information("Extracting {SourceZipFile} to {DestinationDirectory}", sourceZipFile, destinationDirectory);
            ExtractToDirectory(sourceZipFile, destinationDirectory);
            Log.Information("Extracted {SourceZipFile} to {DestinationDirectory}", sourceZipFile, destinationDirectory);
            
            success = true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Could not extract {SourceZipFile} to {DestinationDirectory}", sourceZipFile, destinationDirectory);
        }
        
        if (!success)
        {
            return;
        }
        
        if (string.IsNullOrEmpty(runAfterExtraction))
        {
            return;
        }
        
        var runAfterExtractionPath = Path.Combine(destinationDirectory, runAfterExtraction);
        var runAfterExtractionFullPath = Path.GetFullPath(runAfterExtractionPath);
        
        if (!File.Exists(runAfterExtractionPath))
        {
            Log.Error("File to run after extraction does not exist");
            return;
        }
        
        Log.Information("Running {runAfterExtractionFullPath}", runAfterExtractionFullPath);
        Process.Start(new ProcessStartInfo(runAfterExtractionFullPath)
        {
            UseShellExecute = true,
        });
    }
    
    private static void ExtractToDirectory(string sourceZipFile, string destinationDirectory)
    {
        using var archive = ZipFile.OpenRead(sourceZipFile);
        
        foreach (var entry in archive.Entries)
        {
            var destinationPath = Path.Combine(destinationDirectory, entry.FullName);
            
            var destinationFolder = Path.GetDirectoryName(destinationPath);
            if (destinationFolder == AppDomain.CurrentDomain.BaseDirectory)
            {
                continue;
            }
            
            if (entry.FullName.EndsWith("/"))
            {
                Directory.CreateDirectory(destinationPath);
                continue;
            }
            
            try
            {
                entry.ExtractToFile(destinationPath, true);
            }
            catch (IOException e)
            {
                if (entry.FullName.EndsWith(".sys"))
                {
                    continue;
                }
                
                Log.Error(e, "Could not extract {EntryFullName} to {DestinationPath}", entry.FullName, destinationPath);

                throw;
            }
        }
    }
}