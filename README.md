# FTL Team Starter Mod

[简体中文说明](README.zh-CN.md)

A standalone C# Mod example for FTL Team. It shows how to add a hero, an enemy, an item, six-face dice, a trait, English/Chinese text, and a custom combat rule. You do not need the Unity Editor.

## Requirements

- [.NET SDK 8 or newer](https://dotnet.microsoft.com/download). The project targets .NET Framework 4.7.2 through NuGet reference assemblies.
- A game build with `FTLGame.Unity.dll` and `FTLGame.Combat.dll` in `<game>_Data/Managed`.
- Any terminal: Command Prompt, PowerShell, or a macOS/Linux shell.

## Build and install

From this directory, replace the example path with your game's Managed directory:

```text
dotnet build StarterMod.csproj -c Release "-p:GameManagedDir=C:/Games/FTL Team/ftl_team_Data/Managed"
```

Create `<game>_Data/Mods/StarterMod` and copy `bin/Release/StarterMod.dll` and `info.ini` into it. Enable the Mod in the game's Mods screen.

```text
ftl_team_Data/Mods/StarterMod/
  info.ini
  StarterMod.dll
  preview.png       # Optional locally; required for Steam Workshop publishing
```

## Example content

| Content | Stable ID | Demonstrates |
| --- | --- | --- |
| Apprentice | `sample.starter:unit:apprentice` | Hero, six faces, multi-step face, custom rule |
| Lookout | `sample.starter:unit:lookout` | Enemy, back-row flag, face-specific trait |
| Balanced Stone | `sample.starter:item:balanced-stone` | Item that adds one point to all die faces |
| Opening Strike | `sample.starter:rule:opening-strike` | Custom rule that doubles damage against an unwounded target |

`OnAfterSetup` in `StarterMod.cs` is the registration entry point. Preview queries call `OpeningStrikeRule.ModifyValue` repeatedly, so it must not mutate battle state; use execution-phase context APIs for mutations.

## Make it your own

1. Change `id`, `displayName`, `description`, and `version` in `info.ini`. The `id` must be a unique, stable lowercase namespace such as `yourname.yourmod`.
2. If you rename `StarterMod`, update `info.ini` `name`, the project `AssemblyName` and `RootNamespace`, the C# namespace, and the DLL name. The game looks for `<name>.dll` and `<name>.ModBehaviour`.
3. Rebuild after edits. Do not rename published content IDs casually; saves refer to them.
4. To publish to Steam Workshop, add your own `preview.png` to the installed Mod folder, then upload from the game's Mods screen.

The game's `ModSdk/README.md` contains the full API guide. If you publish this example as a separate repository, provide a link to that guide when it becomes public.

## Notes

- Code Mods run as DLLs inside the game process. Only install Mods from sources you trust.
- This example does not include character or item art. Use `LoadSprite` and `WithArt`/`WithIcon` to supply your own images, and copy those images into the installed Mod folder.
- Run saves record active Mod IDs, versions, and Workshop IDs; the active Mod set must match when continuing a run.
- Choose a license before inviting others to reuse your code, and confirm the rights to any art you add.
