param(
    [string]$Runtime = "win-x64"
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$output = Join-Path $root "dist"
$publish = Join-Path $output "publish"

$resolvedRoot = [System.IO.Path]::GetFullPath($root)
$resolvedOutput = [System.IO.Path]::GetFullPath($output)
if (-not $resolvedOutput.StartsWith($resolvedRoot + [System.IO.Path]::DirectorySeparatorChar)) {
    throw "Refusing to clean an output folder outside the repository."
}

Remove-Item -LiteralPath $output -Recurse -Force -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Path $publish -Force | Out-Null

dotnet publish (Join-Path $root "SchoolApp\SchoolFormApp\SchoolFormApp.csproj") `
    --configuration Release `
    --runtime $Runtime `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:DebugType=None `
    --output $publish

Copy-Item (Join-Path $publish "SSchoolLibrary.exe") (Join-Path $output "SSchoolLibrary-Windows-x64.exe")
Write-Host "Package created: $output\SSchoolLibrary-Windows-x64.exe"
