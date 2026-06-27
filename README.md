# Zombie Shooter

Zombie Shooter is a simple Windows Forms survival game built with C# and .NET 9. Move around the arena, collect ammo, and shoot incoming zombies before your health runs out.

## Controls

- Arrow keys: move the player
- Space: shoot in the current direction
- Restart Game: restart after losing

## Requirements

- Windows
- [.NET 9 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/9.0)

## Build

```powershell
dotnet build "Zombie Shooter\Zombie Shooter.csproj" -c Release
```

The game executable is created at:

```text
Zombie Shooter\bin\Release\net9.0-windows\Zombie Shooter.exe
```

## Setup EXE

The generated setup executable is:

```text
dist\setup\ZombieShooterSetup.exe
```

It installs the game to:

```text
%LOCALAPPDATA%\Programs\Zombie Shooter
```

It also creates Start Menu and Desktop shortcuts.

## Gameplay Fixes

- Fixed zombie vertical movement so zombies chase the player correctly.
- Fixed ammo spawn bounds so ammo appears inside the game window.
- Fixed bullet cleanup so timers do not keep running after bullets leave the screen.
- Fixed restart/game-over flow to avoid hidden windows and duplicate game forms.
- Removed hard-coded bullet screen bounds.
- Cleaned nullable warnings and build warnings.
