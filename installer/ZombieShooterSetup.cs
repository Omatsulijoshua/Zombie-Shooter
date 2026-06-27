using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ZombieShooterInstaller
{
    internal static class Program
    {
        private const string AppName = "Zombie Shooter";
        private const string RuntimeDownloadUrl = "https://dotnet.microsoft.com/download/dotnet/9.0";

        [STAThread]
        private static void Main()
        {
            string installDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Programs",
                AppName);

            Directory.CreateDirectory(installDir);

            Extract("ZombieShooterExe", Path.Combine(installDir, "Zombie Shooter.exe"));
            Extract("ZombieShooterDll", Path.Combine(installDir, "Zombie Shooter.dll"));
            Extract("ZombieShooterDeps", Path.Combine(installDir, "Zombie Shooter.deps.json"));
            Extract("ZombieShooterRuntimeConfig", Path.Combine(installDir, "Zombie Shooter.runtimeconfig.json"));

            string exePath = Path.Combine(installDir, "Zombie Shooter.exe");
            CreateShortcuts(exePath, installDir);

            if (!HasDesktopRuntime())
            {
                Process.Start(RuntimeDownloadUrl);
                MessageBox.Show(
                    "Zombie Shooter was installed, but .NET 9 Desktop Runtime is required before it can run. A download page has been opened.",
                    "Zombie Shooter Setup",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            Process.Start(exePath);
        }

        private static void Extract(string resourceName, string destination)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream input = assembly.GetManifestResourceStream(resourceName))
            {
                if (input == null)
                {
                    throw new InvalidOperationException("Missing setup resource: " + resourceName);
                }

                using (FileStream output = File.Create(destination))
                {
                    input.CopyTo(output);
                }
            }
        }

        private static bool HasDesktopRuntime()
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo("dotnet", "--list-runtimes")
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                using (Process process = Process.Start(info))
                {
                    if (process == null)
                    {
                        return false;
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit(3000);
                    return output.IndexOf("Microsoft.WindowsDesktop.App 9.", StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private static void CreateShortcuts(string exePath, string workingDirectory)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string startMenu = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Microsoft",
                "Windows",
                "Start Menu",
                "Programs",
                AppName);

            Directory.CreateDirectory(startMenu);

            CreateShortcut(Path.Combine(desktop, AppName + ".lnk"), exePath, workingDirectory);
            CreateShortcut(Path.Combine(startMenu, AppName + ".lnk"), exePath, workingDirectory);
        }

        private static void CreateShortcut(string shortcutPath, string targetPath, string workingDirectory)
        {
            Type shellType = Type.GetTypeFromProgID("WScript.Shell");
            if (shellType == null)
            {
                return;
            }

            dynamic shell = Activator.CreateInstance(shellType);
            dynamic shortcut = shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = workingDirectory;
            shortcut.Save();
        }
    }
}
