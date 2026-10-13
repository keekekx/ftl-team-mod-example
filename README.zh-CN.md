# FTL Team Mod 示例工程

[English README](README.md)

这是一个独立的 C# Mod 示例工程，展示如何添加英雄、敌人、物品、六面骰、特性、中英文本和自定义战斗规则。不需要安装 Unity 编辑器。

## 准备

- 安装 [.NET SDK 8 或更新版本](https://dotnet.microsoft.com/download)。工程通过 NuGet 引用程序集，目标框架为 .NET Framework 4.7.2。
- 准备一份游戏构建，在 `<游戏>_Data/Managed` 中找到 `FTLGame.Unity.dll` 和 `FTLGame.Combat.dll`。
- 在任意终端中打开本目录；Windows 命令提示符、PowerShell、macOS/Linux 终端均可。

## 编译和安装

把下例路径替换为游戏的 `Managed` 目录，在本目录运行：

```text
dotnet build StarterMod.csproj -c Release "-p:GameManagedDir=C:/Games/FTL Team/ftl_team_Data/Managed"
```

在游戏的 `<游戏>_Data/Mods/` 下建立 `StarterMod` 文件夹，将编译得到的 `bin/Release/StarterMod.dll` 和本工程的 `info.ini` 复制进去，再在游戏内的 Mod 界面启用。

```text
ftl_team_Data/Mods/StarterMod/
  info.ini
  StarterMod.dll
  preview.png       # 本地安装可选；发布到 Steam 创意工坊时必需
```

## 示例内容

| 内容 | 稳定 ID | 演示点 |
| --- | --- | --- |
| 学徒 | `sample.starter:unit:apprentice` | 英雄、六个骰面、多步骤骰面、自定义规则 |
| 哨兵 | `sample.starter:unit:lookout` | 敌人、后排、指定骰面的特性 |
| 均衡石 | `sample.starter:item:balanced-stone` | 物品、所有骰面点数加成 |
| 先发制人 | `sample.starter:rule:opening-strike` | 对未受伤目标造成双倍伤害的自定义规则 |

`StarterMod.cs` 的 `OnAfterSetup` 是注册入口。预览会反复调用 `OpeningStrikeRule.ModifyValue`，因此这里不能修改战斗状态；实际变更应使用执行阶段的上下文 API。

## 改成自己的 Mod

1. 修改 `info.ini` 中的 `id`、`displayName`、`description` 和 `version`。`id` 必须是唯一、稳定的小写命名空间，例如 `yourname.yourmod`。
2. 如果重命名 `StarterMod`，要同步修改 `info.ini` 的 `name`、工程的 `AssemblyName`/`RootNamespace`、C# 命名空间和 DLL 文件名。游戏会查找 `<name>.dll` 和 `<name>.ModBehaviour`。
3. 修改后重新编译。公开发布的内容 ID 不要随意改名，存档会引用它们。
4. 发布到 Steam 创意工坊前，在安装后的 Mod 文件夹加入自己的 `preview.png`，然后通过游戏内的 Mod 界面上传。

完整 API 说明位于游戏仓库的 `ModSdk/README.md`。独立发布本示例仓库时，可以在该文档公开后补上链接。

## 注意

- 这是代码 Mod；DLL 会在游戏进程中运行，只安装可信来源的 Mod。
- 示例未附带角色和物品美术。可以通过 `LoadSprite` 加载自己的图片，并用 `WithArt`、`WithIcon` 指定；图片也要复制到安装后的 Mod 文件夹。
- 游戏存档记录启用 Mod 的 ID、版本和创意工坊 ID；继续游玩时需使用匹配的 Mod 集合。
- 邀请其他人复用代码前，请自行选择许可证，并确认新增美术的授权。
