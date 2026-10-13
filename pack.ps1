$ErrorActionPreference = "Stop"
$destination = Join-Path $PSScriptRoot "dist/StarterMod"
$assembly = Join-Path $PSScriptRoot "bin/Release/StarterMod.dll"
if (-not (Test-Path -LiteralPath $assembly -PathType Leaf)) {
    throw "StarterMod.dll was not found. Run ./build.ps1 first."
}
New-Item -ItemType Directory -Force -Path $destination | Out-Null

Copy-Item -LiteralPath $assembly -Destination $destination -Force
Copy-Item -LiteralPath "$PSScriptRoot/info.ini" -Destination $destination -Force

$preview = Join-Path $PSScriptRoot "preview.png"
if (Test-Path -LiteralPath $preview) {
    Copy-Item -LiteralPath $preview -Destination $destination -Force
}

Write-Host "Packed Mod: $destination"
