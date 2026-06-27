$ErrorActionPreference = "Stop"
Add-Type -AssemblyName System.Windows.Forms

$source = $PSScriptRoot
$installDir = Join-Path $env:LOCALAPPDATA "Programs\Zombie Shooter"
$startMenuDir = Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs\Zombie Shooter"
$desktopShortcut = Join-Path ([Environment]::GetFolderPath("DesktopDirectory")) "Zombie Shooter.lnk"
$exePath = Join-Path $installDir "Zombie Shooter.exe"

New-Item -ItemType Directory -Force -Path $installDir | Out-Null
New-Item -ItemType Directory -Force -Path $startMenuDir | Out-Null

$files = @(
    "Zombie Shooter.exe",
    "Zombie Shooter.dll",
    "Zombie Shooter.deps.json",
    "Zombie Shooter.runtimeconfig.json"
)

foreach ($file in $files) {
    Copy-Item -LiteralPath (Join-Path $source $file) -Destination (Join-Path $installDir $file) -Force
}

$shell = New-Object -ComObject WScript.Shell

$startShortcut = $shell.CreateShortcut((Join-Path $startMenuDir "Zombie Shooter.lnk"))
$startShortcut.TargetPath = $exePath
$startShortcut.WorkingDirectory = $installDir
$startShortcut.Save()

$desktop = $shell.CreateShortcut($desktopShortcut)
$desktop.TargetPath = $exePath
$desktop.WorkingDirectory = $installDir
$desktop.Save()

$runtimeInstalled = $false
try {
    $runtimeInstalled = (& dotnet --list-runtimes 2>$null) -match "^Microsoft\.WindowsDesktop\.App 9\."
}
catch {
    $runtimeInstalled = $false
}

if (-not $runtimeInstalled) {
    Start-Process "https://dotnet.microsoft.com/download/dotnet/9.0"
    [System.Windows.Forms.MessageBox]::Show(
        "Zombie Shooter was installed, but .NET 9 Desktop Runtime is required before it can run. A download page has been opened.",
        "Zombie Shooter Setup"
    ) | Out-Null
}
else {
    Start-Process -FilePath $exePath
}
