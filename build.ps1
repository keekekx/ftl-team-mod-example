param(
    [Parameter(Mandatory = $true)]
    [string]$GameManagedDir
)

$ErrorActionPreference = "Stop"
$managed = (Resolve-Path -LiteralPath $GameManagedDir).Path
dotnet build "$PSScriptRoot/StarterMod.csproj" --configuration Release `
    -p:GameManagedDir="$managed"
