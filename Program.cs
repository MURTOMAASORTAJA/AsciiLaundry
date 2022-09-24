// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Reflection;

AssertAppExists("wget");
AssertAppExists("yt-dlp");

string videoUrl = "https://www.youtube.com/watch?v=XAB_eNEEpZ4";
var currentDir = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
var videoFilePath = Path.Combine(currentDir, "pesukone.webm");

string RunCommandWithBash(string command)
{
    var psi = new ProcessStartInfo();
    psi.FileName = "/bin/bash";
    psi.Arguments = command;
    psi.RedirectStandardOutput = true;
    psi.UseShellExecute = false;
    psi.CreateNoWindow = true;

    using var process = Process.Start(psi);

    process.WaitForExit();

    var output = process.StandardOutput.ReadToEnd();

    return output;
}

bool AppExists(string appName)
{
    return !string.IsNullOrEmpty(RunCommandWithBash($"which {appName}"));
}

void AssertAppExists(string appName)
{
    if (!AppExists(appName))
    {
        Console.WriteLine($"Can't find {appName}");
        Environment.Exit(0);
    }
}

struct PesukonePaths
{
    public string AppPath { get; set; }
    public string VideoPath { get; set; }
}