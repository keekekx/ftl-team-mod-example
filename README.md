# FTL Team Mod 示例工程 / Starter Mod

这是一个可独立放到 GitHub 的 C# Mod 工程。它展示如何添加一名英雄、一名敌人、一件物品、六面骰、特性、中英文本和自定义战斗规则。工程不需要放进 Unity 项目；编译时只需引用已安装游戏的程序集。

This is a standalone C# Mod example for FTL Team. It demonstrates a hero, an enemy, an item, six-face dice, a trait, English/Chinese text, and a custom combat rule. Unity Editor is not required.

## 准备 / Requirements

- 安装 [.NET SDK 8 或更新版本](https://dotnet.microsoft.com/download)。工程目标框架是 .NET Framework 4.7.2，所需引用程序集由 NuGet 包提供。
- 准备一份与 Mod 对应版本的 Windows 游戏。找到游戏目录中的 `ftl_team_Data/Managed` 文件夹；其中应有 `DiceGame.Unity.dll` 和 `DiceGame.Combat.dll`。
- 在任意终端中打开本目录（Windows 命令提示符、PowerShell、macOS/Linux 终端均可）。

## 编译、打包、安装 / Build, package, install

```text
dotnet build StarterMod.csproj -c Release "-p:GameManagedDir=C:/Games/FTL Team/ftl_team_Data/Managed"
```

将编译得到的 `bin/Release/StarterMod.dll` 与本工程的 `info.ini` 复制到游戏的 `ftl_team_Data/Mods/StarterMod/`，然后在游戏内的 Mod 界面启用它。最终结构应为：

```text
ftl_team_Data/Mods/StarterMod/
  info.ini
  StarterMod.dll
  preview.png       # 可选；发布到 Steam 创意工坊时必需
```

English: run the `dotnet build` command above, copy `bin/Release/StarterMod.dll` and `info.ini` into `ftl_team_Data/Mods/StarterMod`, then enable it in the game's Mods screen. The Steam Workshop publisher requires a `preview.png` in that Mod folder.

## 示例内容 / What this adds

| 内容 / Content | 稳定 ID / Stable ID | 演示点 / Demonstrates |
| --- | --- | --- |
| 学徒 / Apprentice | `sample.starter:unit:apprentice` | 英雄、六个骰面、多步骤骰面、自定义规则 |
| 哨兵 / Lookout | `sample.starter:unit:lookout` | 敌人、后排、指定骰面的特性 |
| 均衡石 / Balanced Stone | `sample.starter:item:balanced-stone` | 物品、所有骰面点数加成 |
| 先发制人 / Opening Strike | `sample.starter:rule:opening-strike` | 自定义战斗规则、英文与中文名称 |

`StarterMod.cs` 的 `OnAfterSetup` 是注册入口。`OpeningStrikeRule` 展示了一个规则：攻击未受伤目标时伤害翻倍。规则查询会被预览反复调用，因此 `ModifyValue` 不应修改战斗状态；需要实际改变状态时使用执行阶段的上下文 API。

## 改成你自己的 Mod / Make it yours

1. 修改 `info.ini` 中的 `id`、`displayName`、`description` 和 `version`。`id` 必须是小写、唯一且稳定的命名空间，例如 `yourname.yourmod`。
2. 如需重命名 `StarterMod`，同时修改 `info.ini` 的 `name`、`StarterMod.csproj` 的 `AssemblyName`/`RootNamespace`、代码的命名空间，以及最终 DLL 文件名。游戏会查找 `<name>.dll` 和 `<name>.ModBehaviour`。
3. 修改内容后重新编译并打包。已经公开发布的内容 ID 不要随意改名：存档会引用它们。
4. 发布到 Steam 创意工坊前，在安装后的 Mod 文件夹加入自己的 `preview.png`，最后在游戏的 Mod 界面上传。

More API details are available in the game's `ModSdk/README.md`. If you publish this directory as a separate GitHub repository, copy that guide alongside it or link to its public location.

## 注意 / Notes

- 这是代码 Mod。游戏会在自身进程中加载 DLL；只安装可信来源的 Mod。
- 当前示例没有附带角色或物品美术。可以使用 `LoadSprite` 加载 Mod 文件夹内的图片，并通过 `WithArt`、`WithIcon` 指定图像；图片也需要随 Mod 一起打包。
- 游戏保存当前启用 Mod 的 ID、版本和创意工坊 ID；继续游玩时需要匹配的 Mod 集合。
- 公开发布本示例仓库前，请自行选择适用的开源许可证，并确认附加图片的授权。
