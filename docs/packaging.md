# Packaging

## Package Identity

| Field | Value |
| --- | --- |
| Package ID | `RevitApi.Contracts` |
| Initial version | `0.1.0` |
| Repository | `https://github.com/Thomash100/Revit-API` |
| Target framework | `net8.0` |

The package contains only Revit-free contracts, DTOs, JSON models, validation models, coordination models, and WriteBack contract types.

## Local Pack

```powershell
dotnet build RevitApi.Contracts.sln --configuration Release
dotnet test tests/RevitApi.Contracts.Tests/RevitApi.Contracts.Tests.csproj --configuration Release
dotnet pack src/RevitApi.Contracts/RevitApi.Contracts.csproj --configuration Release --no-build -p:PackageVersion=0.1.2 --output artifacts/packages
```

## Local Filesystem Feed

GitHub Packages is not required for local package-mode tests. The repository contains a script that creates a filesystem NuGet feed:

```powershell
.\scripts\publish-local-contracts-package.ps1
```

Default output:

```text
artifacts/local-nuget/RevitApi.Contracts.0.1.2.nupkg
```

The script builds `RevitApi.Contracts.sln`, runs the contract tests, packs the contract project, and leaves the package in the local feed. `Thomash100/Revit-TGA-Platform` can consume this feed through its `NuGet.Local.Config`.

## GitHub Packages

The workflow `.github/workflows/package-contracts.yml` packs and publishes `RevitApi.Contracts` to GitHub Packages.

It can be started by:

- pushing a tag such as `v0.1.2` or `revitapi-contracts-v0.1.2`
- running the workflow manually with a package version

Consumers need the GitHub Packages source configured:

```powershell
dotnet nuget add source "https://nuget.pkg.github.com/Thomash100/index.json" `
  --name "github-thomash100" `
  --username "Thomash100" `
  --password "<GITHUB_TOKEN>" `
  --store-password-in-clear-text
```

## Consumer Reference

```xml
<PackageReference Include="RevitApi.Contracts" Version="0.1.2" />
```

`Thomash100/Revit-TGA-Platform` still supports local ProjectReference development, but the target model is a versioned package reference for repeatable CI and team builds.
