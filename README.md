# Revit API Contracts

Reine Schnittstellenbibliothek fuer den Revit-TGA-Datenaustausch.

Dieses Repository enthaelt nur Contracts, DTOs, JSON-Serialisierung, Validation-Modelle und den WriteBack-Vertrag. Revit-Add-in-Code, Ribbon, Commands und App-Module gehoeren in `Thomash100/Revit-TGA-Platform`.

## Inhalt

```text
src/RevitApi.Contracts/
  Domain/       Shaft, Register, Trade, PlanningStatus
  Export/       JSON-Export-DTOs fuer Projekt, Summary, Raeume und Elemente
  Json/         Gemeinsame System.Text.Json-Optionen
  Validation/   Externe Validierungsergebnisse
  WriteBack/    Revit-freier WriteBack-Vertrag
tests/
  RevitApi.Contracts.Tests/
docs/
  ARCHITECTURE.md
  PROJECT_SUMMARY.md
```

## Vertragsregeln

- `UniqueId` ist die fuehrende externe Referenz fuer Validation und WriteBack.
- `ElementId` bleibt nur diagnostische Revit-Kontextinformation.
- DTOs und JSON-Vertraege bleiben Revit-frei und ohne Abhaengigkeit auf `RevitAPI.dll`.
- WriteBack beschreibt nur Parameterupdates und Ergebnisstatus, keine Revit-Transaktionen.

## Build und Tests

```powershell
dotnet build RevitApi.Contracts.sln
dotnet test tests/RevitApi.Contracts.Tests/RevitApi.Contracts.Tests.csproj
dotnet format RevitApi.Contracts.sln --verify-no-changes --no-restore
```

## Konsumenten

`Thomash100/Revit-TGA-Platform` soll diese Bibliothek als Contracts-Schicht referenzieren. Lokal kann das als ProjectReference erfolgen; spaeter ist ein NuGet-Paket sinnvoll.
