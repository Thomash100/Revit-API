[CmdletBinding()]
param(
    [string]$PackageVersion,
    [string]$Configuration = "Release",
    [string]$OutputPath,
    [switch]$SkipTests
)

$ErrorActionPreference = "Stop"

$repositoryRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$versionFile = Join-Path $repositoryRoot "version.json"

if ([string]::IsNullOrWhiteSpace($PackageVersion)) {
    $versionInfo = Get-Content -Raw -LiteralPath $versionFile | ConvertFrom-Json
    $PackageVersion = $versionInfo.package.version
}

if ([string]::IsNullOrWhiteSpace($PackageVersion)) {
    throw "PackageVersion was not provided and could not be read from version.json."
}

if ([string]::IsNullOrWhiteSpace($OutputPath)) {
    $OutputPath = Join-Path $repositoryRoot "artifacts\local-nuget"
}

$solutionPath = Join-Path $repositoryRoot "RevitApi.Contracts.sln"
$testProjectPath = Join-Path $repositoryRoot "tests\RevitApi.Contracts.Tests\RevitApi.Contracts.Tests.csproj"
$contractProjectPath = Join-Path $repositoryRoot "src\RevitApi.Contracts\RevitApi.Contracts.csproj"

New-Item -ItemType Directory -Force -Path $OutputPath | Out-Null

Push-Location $repositoryRoot
try {
    & dotnet build $solutionPath --configuration $Configuration

    if (-not $SkipTests) {
        & dotnet test $testProjectPath --configuration $Configuration --no-build
    }

    & dotnet pack $contractProjectPath `
        --configuration $Configuration `
        --no-build `
        -p:PackageVersion=$PackageVersion `
        --output $OutputPath

    $packagePath = Join-Path $OutputPath "RevitApi.Contracts.$PackageVersion.nupkg"
    if (-not (Test-Path -LiteralPath $packagePath)) {
        throw "Expected package was not created: $packagePath"
    }

    Write-Host "Local package ready: $packagePath"
}
finally {
    Pop-Location
}
